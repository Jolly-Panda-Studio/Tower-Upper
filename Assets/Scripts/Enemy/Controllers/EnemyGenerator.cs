using Lindon.TowerUpper.GameController;
using Lindon.TowerUpper.GameController.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Controller
{
    public class EnemyGenerator
    {
        private List<Enemy> m_Prefabs;

        private EnemyManager m_EnemyManager;

        Transform lastPoint;
        private Coroutine m_coroutine;
        private float m_LastSpawnTime;

        public EnemyGenerator(EnemyManager enemyManager)
        {
            m_EnemyManager = enemyManager;

            m_Prefabs = new List<Enemy>();
        }

        public void AddPrefab(Enemy newPrefab)
        {
            m_Prefabs.Add(newPrefab);
        }

        public void StartSpawn()
        {
            m_coroutine ??= m_EnemyManager.StartCoroutine(SpawnEnemy());
        }

        public void StopSpawn()
        {
            m_EnemyManager.StopCoroutine(m_coroutine);
            m_coroutine = null;
        }

        private IEnumerator SpawnEnemy()
        {
            WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();
            WaitForSeconds waitTime = new WaitForSeconds(2);
            while (true)
            {
                yield return waitFixedUpdate;

                if (!GameRunnig.IsRunning) continue;

                Spawn();
                yield return waitTime;
            }

            //if (!GameRunnig.IsRunning) return null;
            //yield return new WaitForSeconds(2);

            //m_EnemyManager.StartCoroutine(SpawnEnemy());
        }

        private void Spawn()
        {
            bool canSpawnNewEnemy = EnemyCounter.CanSpawnEnemy();
            if (!canSpawnNewEnemy) return;

            var prefab = GetEnemyPrefab();
            var point = GetPoint();

            var enemyIns = Object.Instantiate(prefab, point.StartPoint);
            enemyIns.transform.localPosition = Vector3.zero;

            AddEnemy(enemyIns, point.FinishPoint, point.CenterPoint);
        }

        private Point GetPoint()
        {
            var point = GameManager.Instance.Tower.Components.SpawnPointCreator.Points.RandomItem();
            if (lastPoint != null)
            {
                if (lastPoint == point.StartPoint)
                {
                    return GetPoint();
                }
            }
            lastPoint = point.StartPoint;
            return point;
        }

        private Enemy GetEnemyPrefab() => m_Prefabs.RandomItem();

        internal void AddBossPrefab(Enemy enemyPrefab)
        {
            throw new System.NotImplementedException();
        }

        public void AddEnemy(Enemy enemy, Transform moveTarget, Transform lookAtTarget)
        {
            EnemyCounter.SpawnEnemy();

            enemy.Climbing.SetTargetMove(moveTarget);
            enemy.Climbing.SetLookAt(lookAtTarget);
            enemy.Climbing.Climbimg();

            enemy.OnDie += OnKill;
            enemy.OnFinishClimb += FinishClimbing;
        }

        private void OnKill(Enemy enemy)
        {
            EnemyCounter.KillEnemy();
            enemy.OnDie -= OnKill;
        }

        private void FinishClimbing(Enemy enemy)
        {
            EnemyCounter.ReachEndPath();
            enemy.OnFinishClimb -= FinishClimbing;
        }
    }
}