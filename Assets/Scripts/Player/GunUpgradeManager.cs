using JollyPanda.LastFlag.Database;
using System;
using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{
    public class GunUpgradeManager : MonoBehaviour
    {
        public static GunUpgradeManager instance;

        [SerializeField] private GunSelector gunSelector;

        [Header("Upgrade Configs")]
        [SerializeField] private UpgradeData fireRateUpgrade;
        [SerializeField] private UpgradeData damageUpgrade;
        [SerializeField] private UpgradeData bulletSizeUpgrade;
        [SerializeField] private UpgradeData bulletSpeedUpgrade;
        
        private Gun activeGun;
        private PlayerSaveData saveData;


        public UpgradeData GetBulletSizeUpgradeData()
        {
            return bulletSizeUpgrade;
        }
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private void OnEnable()
        {
            gunSelector.OnGunChange += GunChanged;
        }

        private void OnDisable()
        {
            gunSelector.OnGunChange -= GunChanged;
        }

        private void GunChanged(Gun newGun)
        {
            activeGun = newGun;

            ApplyUpgrades();
        }

        void Start()
        {
            ApplyUpgrades();
        }

        private void LoadPlayerData()
        {
            saveData = SaveSystem.Load();

            fireRateUpgrade.Level = saveData.FireRateLevel;
            damageUpgrade.Level = saveData.DamageLevel;
            bulletSizeUpgrade.Level = saveData.BulletSizeLevel;
            bulletSpeedUpgrade.Level = saveData.BulletSpeedLevel;
        }

        public void ApplyUpgrades()
        {
            LoadPlayerData();

            if (activeGun == null)
            {
                Debug.LogWarning("No active gun assigned to GunUpgradeManager.");
                return;
            }

            activeGun.SetFireRate(fireRateUpgrade.CurrentValue);
            activeGun.SetDamage(damageUpgrade.CurrentValue);
            activeGun.SetBulletSize(bulletSizeUpgrade.CurrentValue);
            activeGun.SetBulletSpeed(bulletSpeedUpgrade.CurrentValue);
        }

        public static bool TryUpgradeFireRate()
        {
            return instance.TryUpgrade(instance.fireRateUpgrade, (lvl) => instance.saveData.FireRateLevel = lvl, ref instance.saveData.Money);
        }

        public static bool TryUpgradeDamage()
        {
            return instance.TryUpgrade(instance.damageUpgrade, (lvl) => instance.saveData.DamageLevel = lvl, ref instance.saveData.Money);
        }

        public static bool TryUpgradeBulletSpeed()
        {
            return instance.TryUpgrade(instance.bulletSpeedUpgrade, (lvl) => instance.saveData.BulletSpeedLevel = lvl, ref instance.saveData.Money);
        }

        public static bool TryUpgradeBulletSize()
        {
            return instance.TryUpgrade(instance.bulletSizeUpgrade, (lvl) => instance.saveData.BulletSizeLevel = lvl, ref instance.saveData.Money);
        }

        private bool TryUpgrade(UpgradeData upgrade, Action<int> onLevelSaved, ref int playerMoney)
        {
            if (!upgrade.CanUpgrade || playerMoney < upgrade.CurrentCost)
                return false;

            playerMoney -= upgrade.CurrentCost;
            upgrade.Level++;
            onLevelSaved(upgrade.Level);
            ApplyUpgrades();
            SaveSystem.Save(saveData);
            return true;
        }
    }
}