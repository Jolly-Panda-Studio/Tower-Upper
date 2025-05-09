using UnityEngine;

namespace JollyPanda.LastFlag.Database
{
    [System.Serializable]
    public class UpgradeData
    {
        public int Level { get; set; } = 0;
        [field: SerializeField] public Value[] Values { get; private set; } = new Value[MaxLevel];

        public static int MaxLevel => 4;
        public bool CanUpgrade => Level < MaxLevel;
        public int CurrentCost
        {
            get
            {
                return Values[Level].cost;
            }
        }

        public float CurrentValue
        {
            get
            {
                return Values[Level].value;
            }
        }

        [System.Serializable]
        public struct Value
        {
            public int cost;
            public float value;
        }
    }

}