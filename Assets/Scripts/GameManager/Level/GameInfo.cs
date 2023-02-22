using System.Collections.Generic;
using UnityEngine;
using Lindon.TowerUpper.Data;
using System.Linq;

namespace Lindon.TowerUpper.GameController.Level
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Lindon/TowerUpper/Data/GameData")]
    public class GameInfo : ScriptableObject
    {
        [SerializeField, Range(1, 10)] private int m_Level;
        [SerializeField] private int m_SpawnPointCount;

        [Header("Enemy")]
        [SerializeField] private int m_MaxEnemyCount = 10;
        private List<int> m_Ids;
        [SerializeField, AssetPopup(typeof(GameModelData))] private List<GameModelData> m_Enemies;
        [SerializeField, AssetPopup(typeof(GameModelData))] private GameModelData m_BossEnemy;

        private void OnValidate()
        {
            m_Ids = m_Enemies.Select(x => x.Id).ToList();
        }

        public int Level => m_Level;
        public List<int> EnemiesId  => m_Ids;
        public bool HasBossFight => m_Level == 10;
        public int MaxEnemyCount => m_MaxEnemyCount;
        public int BossEnemyId => m_BossEnemy.Id;
        public int SpawnPointCount => m_SpawnPointCount;
    }
}