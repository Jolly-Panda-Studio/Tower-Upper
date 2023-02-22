using System;

namespace Lindon.TowerUpper.Manager.Enemies
{
    public static class EnemyCounter
    {
        /// <summary>
        /// Number of enemies killed
        /// </summary>
        public static int KilledEnemy { get; private set; }
        /// <summary>
        /// Number of living enemies
        /// </summary>
        public static int SpawnedEnemy { get; private set; }

        public static int TotalEnemy { get; set; } = 10;

        /// <summary>
        /// Increase the number of enemies killed
        /// </summary>
        public static void KillEnemy()
        {
            KilledEnemy++;

            OnKillEnemy?.Invoke();
        }

        public static bool CanSpawnEnemy()
        {
            return TotalEnemy > SpawnedEnemy;
        }

        /// <summary>
        /// Increasing the number of active enemies
        /// </summary>
        public static void SpawnEnemy() => SpawnedEnemy++;

        public static event Action OnKillEnemy;
    }
}