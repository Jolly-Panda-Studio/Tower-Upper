using JollyPanda.LastFlag.Handlers;
using JollyPanda.LastFlag.PlayerModule;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JollyPanda.LastFlag.EnemyModule
{
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

        [Tooltip("List of enemy prefabs to spawn.")]
        public GameObject[] enemyPrefabs;

        [Tooltip("Time interval between enemy spawns (in seconds).")]
        public float spawnInterval = 2f;

        [Tooltip("Initial pool size per enemy type. (Used only at first spawn)")]
        public int poolSizePerEnemyType = 10;

        [Header("Hierarchy")]
        [Tooltip("Parent transform under which all spawned enemies will be organized.")]
        public Transform enemyParent;

        // Array holding spawn point transforms positioned around the cylinder
        private Transform[] spawnPoints;

        // Dictionary mapping each prefab to its pool of reusable GameObjects
        private Dictionary<GameObject, Queue<GameObject>> enemyPools = new();

        private void OnEnable()
        {
            Informant.OnLose += StopSpawning;
            Informant.OnStart += StartSpawning;
        }

        private void OnDisable()
        {
            Informant.OnLose -= StopSpawning;
            Informant.OnStart -= StartSpawning;
        }

        private void StopSpawning()
        {
            CancelInvoke(nameof(SpawnEnemy));
        }

        public void StartSpawning()
        {
            CancelInvoke(nameof(SpawnEnemy));
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        }

        /// <summary>
        /// Initializes spawn points and starts enemy spawning at intervals.
        /// </summary>
        void Start()
        {
            GenerateSpawnPoints();
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval); // Spawning enemies at regular intervals
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

        /// <summary>
        /// Spawns an enemy at a random spawn point using the pooling system.
        /// </summary>
        void SpawnEnemy()
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject enemy = GetFromPool(prefab);
            if (enemy != null)
            {
                enemy.transform.position = spawnPoints[spawnIndex].position;
                Vector3 directionToCenter = transform.position - enemy.transform.position;
                directionToCenter.y = 0;

                if (directionToCenter != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToCenter);
                    Vector3 euler = targetRotation.eulerAngles;
                    enemy.transform.rotation = Quaternion.Euler(0, euler.y, 0);
                }
                enemy.SetActive(true); // Activate the enemy
            }
        }

        /// <summary>
        /// Retrieves an enemy from the pool, or instantiates a new one if none are available.
        /// </summary>
        GameObject GetFromPool(GameObject prefab)
        {
            if (!enemyPools.ContainsKey(prefab))
            {
                enemyPools[prefab] = new Queue<GameObject>();
            }

            Queue<GameObject> pool = enemyPools[prefab];

            // Search for an inactive object in the pool
            foreach (GameObject enemy in pool)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.SetActive(true); // Reactivate it
                    return enemy;
                }
            }

            // If no inactive object was found, create a new one
            GameObject newEnemy = Instantiate(prefab, enemyParent);
            pool.Enqueue(newEnemy);
            return newEnemy;
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
    }
}
