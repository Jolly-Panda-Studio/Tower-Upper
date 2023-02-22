using Lindon.TowerUpper.GameController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Manager.Enemies
{
    public class EnemyGenerator
    {
        private List<Enemy> m_Prefabs;

        private EnemyManager m_EnemyManager;

        Transform lastPoint;

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
            m_EnemyManager.StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(2);

            Spawn();
            m_EnemyManager.StartCoroutine(SpawnEnemy());
        }

        private void Spawn()
        {
            var prefab = GetEnemyPrefab();
            var point = GetPoint();

            var enemyIns = Object.Instantiate(prefab, point.StartPoint);
            enemyIns.transform.localPosition = Vector3.zero;

            m_EnemyManager.AddEnemy(enemyIns, point.FinishPoint, point.CenterPoint);
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
    }
}