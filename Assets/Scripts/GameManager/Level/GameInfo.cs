using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.GameController.Level
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Lindon/TowerUpper/Data/GameData")]
    public class GameInfo : ScriptableObject
    {
        [SerializeField] private int m_Id;
        [SerializeField, Range(1, 10)] private int m_Level;
        [SerializeField] private List<int> m_EnemiesId;
        [SerializeField] private int m_BossEnemyId;
        [SerializeField] private int m_SpawnPointCount;

        public int Id => m_Id;
        public int Level => m_Level;
        public List<int> EnemiesId  => m_EnemiesId;
        public bool HasBossFight => m_Level == 10;
        public int BossEnemyId => m_BossEnemyId;
        public int SpawnPointCount => m_SpawnPointCount;

        public bool Equals(int id) => m_Id == id;
    }
}