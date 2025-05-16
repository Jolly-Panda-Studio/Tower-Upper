namespace JollyPanda.LastFlag.Database
{
    [System.Serializable]
    public class PlayerSaveData
    {
        public int Money = 0;

        public int FireRateLevel = 0;
        public int DamageLevel = 0;
        public int BulletSizeLevel = 0;
        public int BulletSpeedLevel = 0;

        public int lastWaveIndex = -1;

        public int CurrentWaveIndex => lastWaveIndex + 1;

        public static PlayerSaveData Default => new(50, 0, 0, 0, 0, -1);

        public PlayerSaveData(int money, int fireRateLevel, int damageLevel, int bulletSizeLevel, int bulletSpeedLevel, int lastLevelIndex)
        {
            Money = money;
            FireRateLevel = fireRateLevel;
            DamageLevel = damageLevel;
            BulletSizeLevel = bulletSizeLevel;
            BulletSpeedLevel = bulletSpeedLevel;
            this.lastWaveIndex = lastLevelIndex;
        }
    }

}