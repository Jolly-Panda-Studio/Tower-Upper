using Lindon.TowerUpper.GameController;
using Lindon.TowerUpper.GameController.Events;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : UIPopup
{
    [Header("Buttons")]
    [SerializeField] private Button m_ClaimButton;
    [SerializeField] private Button m_ClaimADButton;

    [Header("Rewards")]
    [SerializeField] private TMP_Text m_GoltAmountText;

    private int m_GoltRewardAmount;

    protected override void SetValues()
    {
        m_GoltRewardAmount = RewardCalculator.GetGoldReward();

        m_GoltAmountText.SetText($"{m_GoltRewardAmount}");
    }

    protected override void SetValuesOnSceneLoad()
    {
        m_ClaimButton.onClick.RemoveAllListeners();
        m_ClaimButton.onClick.AddListener(Claim);

        m_ClaimADButton.onClick.RemoveAllListeners();
        m_ClaimADButton.onClick.AddListener(ClaimAd);
    }

    private void Claim()
    {
        GoldCalculator.GoldAmount += m_GoltRewardAmount;
        m_GoltRewardAmount = 0;
        UserInterfaceManager.OnBackPressed();
        UserInterfaceManager.Open<HomePage>();
    }

    private void ClaimAd()
    {
        Debug.Log("Not yet!!");
    }
}
