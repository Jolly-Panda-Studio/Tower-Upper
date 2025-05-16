using UnityEngine;

namespace JollyPanda.LastFlag.EnemyModule
{
    public class WaveDataGenerator : MonoBehaviour
    {
        [Header("Base Settings")]
        public Enemy[] allEnemies;

        [Min(1)] public int maxEnemyTypesPerWave = 3;
        [Min(5)] public int minCount = 5;
        [Min(10)] public int maxCount = 50;
        [Min(0.2f)] public float minSpawnInterval = 0.5f;
        public float maxSpawnInterval = 2.5f;
        public float baseSpeedMin = 1f;
        public float baseSpeedMax = 2f;
        public float delayAfterWave = 5f;

        public WaveData GenerateWave(int level)
        {
            int enemyCount = Mathf.Clamp(minCount + level, minCount, maxCount);

            float minSpeed = baseSpeedMin + Mathf.Min(level * 0.1f, 4f);
            float maxSpeed = baseSpeedMax + Mathf.Min(level * 0.15f, 5f);

            float spawnInterval = Mathf.Max(maxSpawnInterval - (level * 0.05f), minSpawnInterval);

            int enemyTypeCount = Mathf.Min(1 + level / 10, maxEnemyTypesPerWave);
            Enemy[] selectedEnemies = new Enemy[enemyTypeCount];
            for (int i = 0; i < enemyTypeCount; i++)
            {
                int index = (level + i) % allEnemies.Length;
                selectedEnemies[i] = allEnemies[index];
            }

            float delay = Mathf.Max(delayAfterWave - (level * 0.05f), 1f);

            return new WaveData(selectedEnemies, enemyCount, spawnInterval, minSpeed, maxSpeed, delay);
        }
    }
}
