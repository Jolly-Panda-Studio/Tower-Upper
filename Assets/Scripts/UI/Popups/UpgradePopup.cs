using System.Globalization;
using JollyPanda.LastFlag.Database;
using JollyPanda.LastFlag.Handlers;
using JollyPanda.LastFlag.PlayerModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJUtilities.UI
{
    public class UpgradePopup : UIPopup
    {
        [Header("Money")] 
        [SerializeField] private TMP_Text moneyText;

        [Header("Buttons")] 
        [SerializeField] private Button fireRateUpgradeButton;
        [SerializeField] private Button damageUpgradeButton;
        [SerializeField] private Button bulletSpeedUpgradeButton;
        [SerializeField] private Button bulletSizeUpgradeButton;
        
        [Header("BuyCostText")] 
        [SerializeField] private UpgradeProgressConfig fireRateUpgradeProgress;
        [SerializeField] private UpgradeProgressConfig damageUpgradeProgress;
        [SerializeField] private UpgradeProgressConfig bulletSpeedUpgradProgress;
        [SerializeField] private UpgradeProgressConfig bulletSizeUpgradeProgress;

        protected override void OnEnable()
        {
            base.OnEnable();
            Informant.OnProfileChange += ProfileChange;
        }

        private void OnDisable()
        {
            Informant.OnProfileChange -= ProfileChange;
        }
        public override void OnSetValues()
        {
            BackButtonConfig();
            
            fireRateUpgradeButton.onClick.RemoveAllListeners();
            fireRateUpgradeButton.onClick.AddListener(FireRateUpgradeButtonClicked);
            
            damageUpgradeButton.onClick.RemoveAllListeners();
            damageUpgradeButton.onClick.AddListener(DamageUpgradeButtonClicked);
            
            bulletSpeedUpgradeButton.onClick.RemoveAllListeners();
            bulletSpeedUpgradeButton.onClick.AddListener(BulletSpeedUpgradeButtonClicked);
            
            bulletSizeUpgradeButton.onClick.RemoveAllListeners();
            bulletSizeUpgradeButton.onClick.AddListener(BulletSizeUpgradeButtonClicked);

            LoadCurrentUpgradeStatus();
            Informant.GetUpdatedData();
        }

        private void BulletSizeUpgradeButtonClicked()
        {
            var canUpgrade = GunUpgradeManager.TryUpgradeBulletSize();
            if (canUpgrade)
            {
                var upgradeData = GunUpgradeManager.instance.GetBulletSizeUpgradeData();
                bulletSizeUpgradeProgress.HandleUpgrade(upgradeData);
            }
        }

        private void BulletSpeedUpgradeButtonClicked()
        {
            var canUpgrade = GunUpgradeManager.TryUpgradeBulletSpeed();
            if (canUpgrade)
            {
                var upgradeData = GunUpgradeManager.instance.GetBulletSpeedUpgradeData();
                bulletSpeedUpgradProgress.HandleUpgrade(upgradeData);
            }
        }

        private void DamageUpgradeButtonClicked()
        {
            var canUpgrade = GunUpgradeManager.TryUpgradeDamage();
            if (canUpgrade)
            {
                var upgradeData = GunUpgradeManager.instance.GetDamageUpgradeData();
                damageUpgradeProgress.HandleUpgrade(upgradeData);
            }
        }

        private void FireRateUpgradeButtonClicked()
        {
            var canUpgrade = GunUpgradeManager.TryUpgradeFireRate();
            if (canUpgrade)
            {
                var upgradeData = GunUpgradeManager.instance.GetFireRateUpgradeData();
                fireRateUpgradeProgress.HandleUpgrade(upgradeData);
            }
        }

        private void LoadCurrentUpgradeStatus()
        {
            var gunUpgradeManager = GunUpgradeManager.instance;
            
            var upgradeData = gunUpgradeManager.GetFireRateUpgradeData();
            fireRateUpgradeProgress.HandleUpgrade(upgradeData);
            
            upgradeData = gunUpgradeManager.GetDamageUpgradeData();
            damageUpgradeProgress.HandleUpgrade(upgradeData);
            
            upgradeData = gunUpgradeManager.GetBulletSpeedUpgradeData();
            bulletSpeedUpgradProgress.HandleUpgrade(upgradeData);
            
            upgradeData = gunUpgradeManager.GetBulletSizeUpgradeData();
            bulletSizeUpgradeProgress.HandleUpgrade(upgradeData);
            
        }

        public override void OnAwake()
        {
            
        }
        
        private void ProfileChange(PlayerSaveData playerSaveData)
        {
            long currentMoney = playerSaveData.Money;
            moneyText.text = FormatMoney(currentMoney);
        }

        private string FormatMoney(long money)
        {
            return money.ToString("N0", CultureInfo.InvariantCulture);
        }

    }
}