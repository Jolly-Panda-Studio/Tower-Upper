using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameResault
    {
        public static event Action OnWin;
        public static event Action OnLose;
    }
}