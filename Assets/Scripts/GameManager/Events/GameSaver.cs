using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class GameSaver
    {
        public static event Action OnSave;

        public static void Save()
        {
            OnSave?.Invoke();
        }
    }

    public static class GameLoader
    {
        public static event Action OnLoad;

        public static void Load()
        {
            OnLoad?.Invoke();
        }
    }
}