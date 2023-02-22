using System;

namespace Lindon.TowerUpper.GameController.Events
{
    public static class ReturnHome
    {
        public static event Action OnReturnHome;

        public static void Return()
        {
            OnReturnHome?.Invoke();
        }
    }
}