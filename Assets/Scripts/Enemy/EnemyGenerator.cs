using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator
{
    private EnemyManager m_EnemyManager;

    List<Point> m_Points = new List<Point>();
    Transform lastPoint;

    public EnemyGenerator(EnemyManager enemyManager)
    {
        m_EnemyManager = enemyManager;

        m_Points = Tower.Instance.Components.SpawnPointCreator.Points;
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
        var prefab = m_EnemyManager.GetEenemyPrefabs();
        var point = GetPoint();

        var enemyIns = Object.Instantiate(prefab, point.StartPoint);
        enemyIns.transform.localPosition = Vector3.zero;

        m_EnemyManager.AddEnemy(enemyIns, point.FinishPoint);
    }

    private Point GetPoint()
    {
        var point = m_Points.RandomItem();
        if(lastPoint != null)
        {
            if(lastPoint == point.StartPoint)
            {
                return GetPoint();
            }
        }
        lastPoint = point.StartPoint; 
        return point;
    }
}
