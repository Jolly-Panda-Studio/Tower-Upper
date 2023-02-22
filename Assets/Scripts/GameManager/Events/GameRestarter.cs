using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameRestarter
    {
        public static event Action OnRestartGame;

        public static void RestartGame()
        {
            OnRestartGame?.Invoke();
        }
    }
}