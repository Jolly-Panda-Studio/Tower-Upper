using UnityEngine;

namespace JollyPanda.LastFlag.EnemyModule
{
    [CreateAssetMenu(fileName = "NewWaveData", menuName = "LastFlag/Wave Data", order = 0)]
    public class WaveData : ScriptableObject
    {
        [Tooltip("Enemies to spawn in this wave.")]
        [field: SerializeField] public Enemy[] EnemyPrefabs { get; private set; }

        [Tooltip("Number of enemies to spawn in this wave.")]
        [field: SerializeField, Min(0)] public int EnemyCount { get; private set; }

        [Tooltip("Time between spawns in this wave.")]
        [field: SerializeField, Min(0)] public float SpawnInterval { get; private set; } = 1f;

        [Tooltip("Minimum speed for enemies in this wave.")]
        [field: SerializeField, Min(0)] public float MinSpeed { get; private set; } = 1f;

        [Tooltip("Maximum speed for enemies in this wave.")]
        [field: SerializeField, Min(0)] public float MaxSpeed { get; private set; } = 2f;

        [Tooltip("Time to wait before starting the next wave.")]
        [field: SerializeField, Min(0)] public float DelayAfterWave { get; private set; } = 5f;
    }
}
