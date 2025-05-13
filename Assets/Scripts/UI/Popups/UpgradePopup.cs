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
        [SerializeField] private TMP_Text fireRateUpgradeCostText;
        [SerializeField] private TMP_Text damageUpgradeCostText;
        [SerializeField] private TMP_Text bulletSpeedUpgradeCostText;
        [SerializeField] private TMP_Text bulletSizeUpgradeCostText;

        private void OnEnable()
        {
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
        }

        private void BulletSizeUpgradeButtonClicked()
        {
            var tmpUpgradeData = GunUpgradeManager.instance.GetBulletSizeUpgradeData();
            Debug.Log("CurrentCost=" + tmpUpgradeData.CurrentCost + "  Level=" + tmpUpgradeData.Level);
            
            var canUpgrade = GunUpgradeManager.TryUpgradeBulletSize();
            Debug.Log("canUpgrade=" + canUpgrade);
            Debug.Log("AFTER CurrentCost=" + tmpUpgradeData.CurrentCost + "  Level=" + tmpUpgradeData.Level);
            
            if (canUpgrade)
            {
                var upgradeData = GunUpgradeManager.instance.GetBulletSizeUpgradeData();
                bulletSizeUpgradeCostText.text = upgradeData.CurrentCost.ToString();
            }
        }

        private void BulletSpeedUpgradeButtonClicked() => GunUpgradeManager.TryUpgradeBulletSpeed();
        private void DamageUpgradeButtonClicked() => GunUpgradeManager.TryUpgradeDamage();
        private void FireRateUpgradeButtonClicked() => GunUpgradeManager.TryUpgradeFireRate();

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