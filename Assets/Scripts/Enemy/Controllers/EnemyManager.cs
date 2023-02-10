using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;

    [Header("Componenets")]
    [SerializeField] private EnemyGenerator m_EnemyGenerator;

    private void Start()
    {
        m_EnemyGenerator = new EnemyGenerator(this);

        m_EnemyGenerator.StartSpawn();
    }

    public void AddEnemy(Enemy enemy,Transform moveTarget,Transform lookAtTarget)
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

    public Enemy GetEenemyPrefabs() => enemyPrefabs.RandomItem();
}
