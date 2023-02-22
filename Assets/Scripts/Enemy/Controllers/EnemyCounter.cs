using System;

namespace Lindon.TowerUpper.Manager.Enemies
{
    public static class EnemyCounter
    {
        /// <summary>
        /// Number of enemies killed
        /// </summary>
        private static int KilledEnemy { get; set; }
        /// <summary>
        /// Number of living enemies
        /// </summary>
        private static int SpawnedEnemy { get; set; }
        public static int TotalEnemy { get; set; } = 10;
        private static int ReachedEnemy { get; set; }

        public static event Action<int,int> OnKillEnemy;
        public static event Action<int> OnEnemyReached;

        /// <summary>
        /// Increase the number of enemies killed
        /// </summary>
        public static void KillEnemy() => OnKillEnemy?.Invoke(++KilledEnemy, TotalEnemy);

        public static bool CanSpawnEnemy() => TotalEnemy > SpawnedEnemy;

        /// <summary>
        /// Increasing the number of active enemies
        /// </summary>
        public static void SpawnEnemy() => SpawnedEnemy++;

        public static void ReachEndPath() => OnEnemyReached?.Invoke(++ReachedEnemy);

        public static void Reset()
        {
            KilledEnemy = 0;
            SpawnedEnemy = 0;
            TotalEnemy = 0;
            ReachedEnemy = 0;
        }
    }
}