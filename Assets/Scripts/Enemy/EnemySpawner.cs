using JollyPanda.LastFlag.Database;
using JollyPanda.LastFlag.Handlers;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JollyPanda.LastFlag.EnemyModule
{
    public delegate void WaveEventHandler(int waveIndex);

    /// <summary>
    /// Manages spawning of enemy GameObjects around a cylindrical area using an object pooling system.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [Tooltip("Number of points around the cylinder where enemies can spawn.")]
        public int numberOfSpawnPoints = 8;

        [Tooltip("Radius of the circular spawn area around the center.")]
        public float radius = 2f;

        [Tooltip("Height of the visual gizmo for spawn lines.")]
        public float height = 5f;

        [Tooltip("Reference to the cylinder renderer (optional, not used in logic).")]
        public Renderer cylinderRenderer;

        [Header("Hierarchy")]
        [Tooltip("Parent transform under which all spawned enemies will be organized.")]
        public Transform enemyParent;

        // Array holding spawn point transforms positioned around the cylinder
        private Transform[] spawnPoints;

        [Header("Wave Settings")]
        [SerializeField] private WaveDataGenerator waveDataGenerator;
        private WaveData currentWaveData;
        private int spawnedInWave = 0;
        private bool waveInProgress = false;

        private int aliveEnemies = 0;
        private int totalEnemies = 0;

        public event WaveEventHandler OnWaveStart;
        public event WaveEventHandler OnWaveEnd;

        // Dictionary mapping each prefab to its pool of reusable GameObjects
        private Dictionary<Enemy, Queue<Enemy>> enemyPools = new();

        // Array to track cooldowns for each spawn point
        private float[] spawnCooldowns;

        private void OnEnable()
        {
            Informant.OnLose += StopSpawning;
            Informant.OnStart += StartSpawningWave;
            Informant.OnChangeUIPage += DestroyEnemies;
        }

        private void OnDisable()
        {
            Informant.OnLose -= StopSpawning;
            Informant.OnStart -= StartSpawningWave;
            Informant.OnChangeUIPage -= DestroyEnemies;
        }

        private void DestroyEnemies(PageType type)
        {
            if (type == PageType.Home)
            {
                DestroyEnemies();

                StopSpawning();
            }
        }

        private void DestroyEnemies()
        {
            foreach (var enemyQueue in enemyPools.Values)
            {
                foreach (var enemy in enemyQueue)
                {
                    enemy.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Initializes spawn points and starts enemy spawning at intervals.
        /// </summary>
        void Start()
        {
            GenerateSpawnPoints();
            spawnCooldowns = new float[numberOfSpawnPoints];
            //StartSpawningWave();
        }

        private void Update()
        {
            for (int i = 0; i < spawnCooldowns.Length; i++)
            {
                if (spawnCooldowns[i] > 0f)
                {
                    spawnCooldowns[i] -= Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// Generates spawn points in a circular layout at the bottom of the cylinder.
        /// </summary>
        void GenerateSpawnPoints()
        {
            spawnPoints = new Transform[numberOfSpawnPoints];
            for (int i = 0; i < numberOfSpawnPoints; i++)
            {
                float angle = i * Mathf.PI * 2f / numberOfSpawnPoints;
                Vector3 position = new(
                    Mathf.Cos(angle) * radius,
                    -height / 2f,
                    Mathf.Sin(angle) * radius
                );

                GameObject point = new GameObject("SpawnPoint_" + i);
                point.transform.position = transform.position + position;
                point.transform.parent = transform;
                spawnPoints[i] = point.transform;
            }
        }

        #region Spawn Enemy

        void SpawnEnemy()
        {
            if (spawnedInWave >= currentWaveData.EnemyCount)
                return;

            int spawnIndex = GetValidSpawnPointIndex();
            if (spawnIndex == -1)
            {
                Debug.Log("No valid spawn point available.");
                return;
            }

            SpawnEnemyAtPoint(spawnIndex);
        }

        int GetValidSpawnPointIndex()
        {
            int attempts = 0;
            int spawnIndex = -1;
            int maxAttempts = spawnPoints.Length * 2; // Try to avoid infinite loop

            while (attempts < maxAttempts)
            {
                spawnIndex = Random.Range(0, spawnPoints.Length); // Randomly select a spawn point
                if (spawnCooldowns[spawnIndex] <= 0) // Check if cooldown is over
                {
                    return spawnIndex; // Found a valid point
                }
                attempts++;
            }

            return -1; // No valid spawn point found after max attempts
        }

        void SpawnEnemyAtPoint(int spawnIndex)
        {
            var prefab = currentWaveData.EnemyPrefabs[Random.Range(0, currentWaveData.EnemyPrefabs.Length)];
            var enemy = GetFromPool(prefab);

            if (enemy == null) return;

            PositionEnemyAtSpawnPoint(enemy, spawnIndex);
            SetEnemyBehavior(enemy);

            spawnCooldowns[spawnIndex] = 0.9f; // Reset cooldown for this spawn point

            spawnedInWave++;
        }

        void PositionEnemyAtSpawnPoint(Enemy enemy, int spawnIndex)
        {
            enemy.transform.position = spawnPoints[spawnIndex].position;

            Vector3 directionToCenter = transform.position - enemy.transform.position;
            directionToCenter.y = 0;
            if (directionToCenter != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(directionToCenter);
                enemy.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
            }
        }

        void SetEnemyBehavior(Enemy enemy)
        {
            enemy.SetClimbSpeed(Random.Range(currentWaveData.MinSpeed, currentWaveData.MaxSpeed));
            enemy.SetHealth(Random.Range(currentWaveData.MinHealth, currentWaveData.MaxHealth));
            enemy.SetDeadAction((killedEnemy) =>
            {
                aliveEnemies--;
                if (aliveEnemies <= 0 && waveInProgress)
                {
                    EndCurrentWave();
                }
                Informant.NotifyEnemyKilled(aliveEnemies, totalEnemies);
            });
            enemy.SetReachAction((reachedEnemy) =>
            {
                Informant.NotifyEnemyReachedTop(reachedEnemy, Mathf.Abs(spawnedInWave - aliveEnemies));
            });
        }

        /// <summary>
        /// Retrieves an enemy from the pool, or instantiates a new one if none are available.
        /// </summary>
        Enemy GetFromPool(Enemy prefab)
        {
            if (!enemyPools.ContainsKey(prefab))
            {
                enemyPools[prefab] = new Queue<Enemy>();
            }

            Queue<Enemy> pool = enemyPools[prefab];

            // Search for an inactive object in the pool
            foreach (var enemy in pool)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    enemy.gameObject.SetActive(true); // Reactivate it
                    return enemy;
                }
            }

            // If no inactive object was found, create a new one
            Enemy newEnemy = Instantiate(prefab, enemyParent);
            pool.Enqueue(newEnemy);
            return newEnemy;
        }

        #endregion

        private void StartSpawningWave()
        {
            DestroyEnemies();

            int currentWaveIndex = SaveSystem.GetCurrentWaveIndex();
            currentWaveData = waveDataGenerator.GenerateWave(currentWaveIndex);

            if (currentWaveData.EnemyPrefabs == null || currentWaveData.EnemyPrefabs.Length == 0)
            {
                Debug.LogError($"Generated wave {currentWaveIndex} has no enemy prefabs!");
                return;
            }

            if (currentWaveData.EnemyPrefabs == null || currentWaveData.EnemyPrefabs.Length == 0)
            {
                Debug.LogError($"Wave {currentWaveIndex} has no enemy prefabs!");
                return;
            }

            spawnedInWave = 0;
            waveInProgress = true;

            aliveEnemies = currentWaveData.EnemyCount;
            totalEnemies = currentWaveData.EnemyCount;

            OnWaveStart?.Invoke(currentWaveIndex);
            Informant.NotifyStartWave(currentWaveIndex);

            CancelInvoke(nameof(SpawnEnemy));
            InvokeRepeating(nameof(SpawnEnemy), 0f, currentWaveData.SpawnInterval);
            Informant.NotifyEnemyKilled(aliveEnemies, totalEnemies);
        }

        private void EndCurrentWave()
        {
            CancelInvoke(nameof(SpawnEnemy));
            waveInProgress = false;

            int waveIndex = SaveSystem.GetCurrentWaveIndex();
            OnWaveEnd?.Invoke(waveIndex);
            Informant.NotifyFinishWave(waveIndex, Mathf.Abs(spawnedInWave - aliveEnemies));

            Invoke(nameof(StartSpawningWave), currentWaveData.DelayAfterWave);
        }

        private void StopSpawning()
        {
            CancelInvoke(nameof(SpawnEnemy));
            waveInProgress = false;
        }


        /// <summary>
        /// Draws gizmos in the editor to visualize spawn points and their vertical range.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (numberOfSpawnPoints <= 0) return;

            Gizmos.color = Color.green;
            for (int i = 0; i < numberOfSpawnPoints; i++)
            {
                float angle = i * Mathf.PI * 2f / numberOfSpawnPoints;
                Vector3 basePos = transform.position + new Vector3(Mathf.Cos(angle) * radius, -height / 2f, Mathf.Sin(angle) * radius);
                Vector3 topPos = basePos + Vector3.up * height;

                Gizmos.DrawSphere(basePos, 0.1f);
                Gizmos.DrawLine(basePos, topPos);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Force Skip To Next Wave")]
        public void ForceSkipToNextWave()
        {
            if (!waveInProgress) return;

            int waveIndex = SaveSystem.GetCurrentWaveIndex();
            Debug.LogWarning($"[Debug] Force skipping wave {waveIndex}");

            foreach (Transform child in enemyParent)
            {
                child.gameObject.SetActive(false);
            }

            aliveEnemies = 0;
            EndCurrentWave();
        }
#endif
    }
}
