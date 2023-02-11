using System;

public static class EnemyCounter
{
    /// <summary>
    /// Number of enemies killed
    /// </summary>
    public static int KilledEnemy { get; private set; }
    /// <summary>
    /// Number of living enemies
    /// </summary>
    public static int AliveEnemy { get; private set; }

    public static int TotalEnemy { get; private set; } = 10;

    /// <summary>
    /// Increase the number of enemies killed
    /// </summary>
    public static void KillEnemy()
    {
        KilledEnemy++;

        OnKillEnemy?.Invoke();
    }

    /// <summary>
    /// Increasing the number of active enemies
    /// </summary>
    public static void SpawnEnemy() => AliveEnemy++;

    public static event Action OnKillEnemy;
}
