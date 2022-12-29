using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    List<Transform> m_SpawnPoints = new List<Transform>();
    Transform lastPoint;

    private void Start()
    {
        m_SpawnPoints = Tower.Instance.Components.SpawnPointCreator.Points;
        StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2);

        Spawn();
        StartCoroutine(SpawnEnemy());
    }

    private void Spawn()
    {
        var prefab = enemyPrefabs.RandomItem();
        var spawnPoint = GetPoint();

        var enemyIns = Instantiate(prefab, spawnPoint);
        enemyIns.transform.localPosition = Vector3.zero;
    }

    private Transform GetPoint()
    {
        var point = m_SpawnPoints.RandomItem();
        if(lastPoint != null)
        {
            if(lastPoint == point)
            {
                return GetPoint();
            }
        }
        lastPoint = point; 
        return point;
    }
}
