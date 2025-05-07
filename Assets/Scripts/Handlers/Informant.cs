using JollyPanda.LastFlag.EnemyModule;
using System;

namespace JollyPanda.LastFlag.Handlers
{
    public static class Informant
    {
        public static event Action<Enemy> OnEnemyReachedTop;
        public static event Action OnLose;
        public static event Action OnStart;

        public static void NotifyEnemyReachedTop(Enemy enemy)
        {
            OnEnemyReachedTop?.Invoke(enemy);
            OnLose?.Invoke();
        }
    }
}