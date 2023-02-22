using Lindon.TowerUpper.GameController.Level;
using Lindon.TowerUpper.Initilizer;
using Lindon.TowerUpper.Manager.Enemies;
using UnityEngine;

namespace Lindon.TowerUpper.GameController
{
    public class GameManager : MonoBehaviour, IInitilizer
    {
        [Header("Component")]
        [SerializeField] private Tower m_Tower;
        [SerializeField] private LevelInfo m_LevelInfo;
        [SerializeField] private EnemyManager m_EnemyManager;

        public static GameManager Instance { get; private set; }
        public Tower Tower => m_Tower;
        public LevelInfo LevelInfo => m_LevelInfo;
        public EnemyManager EnemyManager => m_EnemyManager;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
