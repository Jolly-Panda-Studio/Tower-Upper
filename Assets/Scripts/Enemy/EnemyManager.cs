using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] private EnemyGenerator m_EnemyGenerator;


    [SerializeField] private List<Enemy> enemyPrefabs;
    private List<Enemy> enemies;

    private void Start()
    {
        enemies = new List<Enemy>();

        m_EnemyGenerator = new EnemyGenerator(this);


        m_EnemyGenerator.StartSpawn();
    }

    public void AddEnemy(Enemy enemy,Transform moveTarget,Transform lookAtTarget)
    {
        enemy.SetTargetMove(moveTarget).SetLookAt(lookAtTarget).Climb();
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void DoClib()
    {

    }

    public Enemy GetEenemyPrefabs() => enemyPrefabs.RandomItem();
}
