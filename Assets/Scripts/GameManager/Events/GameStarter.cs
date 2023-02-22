using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameStarter
    {
        public static event Action OnStartGame;

        public static void StartGame()
        {
            OnStartGame?.Invoke();
        }
    }
}