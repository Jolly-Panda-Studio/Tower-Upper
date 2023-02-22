using Lindon.TowerUpper.GameController.Level;
using Lindon.TowerUpper.Initilizer;
using System;
using System.Collections.Generic;
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
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame += StartGame;
        }

        private void StartGame()
        {
            m_EnemyGenerator.StartSpawn();
        }

        public void AddEnemy(Enemy enemy, Transform moveTarget, Transform lookAtTarget)
        {
            EnemyCounter.SpawnEnemy();

            enemy.SetTargetMove(moveTarget).SetLookAt(lookAtTarget).Climb();

            enemy.onDie += OnKill;
        }

        private void OnKill(Enemy enemy)
        {
            EnemyCounter.KillEnemy();

            enemy.onDie -= OnKill;
        }
    }
}