using Lindon.TowerUpper.Manager.Enemies;
using Lindon.TowerUpper.Profile;
using System;

namespace Lindon.TowerUpper.GameController
{
    public static class GoldCalculator
    {
        private static int m_Gold;

        public static int GoldAmount
        {
            get
            {
                return m_Gold;
            }
            set
            {
                m_Gold = value;
                GoldChanged?.Invoke();
            }
        }

        public static event Action GoldChanged;
    }

    public static class RewardCalculator
    {
        public static int GetGoldReward()
        {
            return EnemyCounter.KilledEnemy * 10;
        }
    }
}