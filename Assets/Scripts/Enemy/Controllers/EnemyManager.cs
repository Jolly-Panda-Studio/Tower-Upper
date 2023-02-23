using Lindon.TowerUpper.GameController.Events;
using System;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Controller
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
            GameRestarter.OnRestartGame += FinishGame;
            GameRunnig.OnChange += OnChangeRunnig;
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame -= StartGame;
            GameFinisher.OnFinishGame -= FinishGame;
            GameRestarter.OnRestartGame -= FinishGame;
            GameRunnig.OnChange -= OnChangeRunnig;
        }

        private void StartGame()
        {
            m_EnemyGenerator.StartSpawn();
        }

        private void FinishGame()
        {
            EnemyCounter.Reset();
        }

        private void OnChangeRunnig(bool state)
        {
            if (state)
            {
                m_EnemyGenerator.StartSpawn();
            }
            else
            {
                m_EnemyGenerator.StopSpawn();
            }
        }
    }
}