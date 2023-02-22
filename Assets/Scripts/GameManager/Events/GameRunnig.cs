using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameRunnig
    {
        private static bool isRunning = false;

        public static bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                OnChange?.Invoke(value);
            }
        }

        public static event Action<bool> OnChange;
    }
}