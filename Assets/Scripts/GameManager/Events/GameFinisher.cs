using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameFinisher
    {
        public static event Action OnFinishGame;

        public static void FinishGame()
        {
            OnFinishGame?.Invoke();
        }
    }
}