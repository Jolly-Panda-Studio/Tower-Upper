using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Lindon/TowerUpper/Data/GameData")]
    public class GameInfo : ScriptableObject
    {
        [SerializeField, Range(1, 10)] private int m_Level;
        [SerializeField] private int m_SpawnPointCount;

        [Header("Enemy")]
        [SerializeField] private int m_MaxEnemyCount = 10;
        [SerializeField] private List<int> m_EnemiesId;
        [SerializeField] private int m_BossEnemyId;

        public int Level => m_Level;
        public List<int> EnemiesId  => m_EnemiesId;
        public bool HasBossFight => m_Level == 10;
        public int MaxEnemyCount => m_MaxEnemyCount;
        public int BossEnemyId => m_BossEnemyId;
        public int SpawnPointCount => m_SpawnPointCount;
    }
}