using Lindon.TowerUpper.GameController.Events;
using UnityEngine;

namespace Lindon.TowerUpper.Manager.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyGenerator Generator => m_EnemyGenerator;

        [Header("Componenets")]
        private EnemyGenerator m_EnemyGenerator;

        private void Start()
        {
            LoadControllers();
        }

        private void LoadControllers()
        {
            m_EnemyGenerator = new EnemyGenerator(this);
        }

        private void OnEnable()
        {
            GameStarter.OnStartGame += StartGame;
            GameFinisher.OnFinishGame += FinishGame;
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame -= StartGame;
            GameFinisher.OnFinishGame -= FinishGame;
        }

        private void StartGame()
        {
            m_EnemyGenerator.StartSpawn();
        }

        private void FinishGame()
        {
            EnemyCounter.Reset();
        }
    }
}