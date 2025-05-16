using System;
using JollyPanda.LastFlag.EnemyModule;
using JollyPanda.LastFlag.Handlers;
using UnityEngine;
using UnityEngine.UI;
using MJUtilities.UI;
using TMPro;

public class LosePopup : UIPopup
{
    public static LosePopup instance;

    [SerializeField] private TMP_Text coinText;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    
    public override void OnAwake()
    {
        Informant.OnEarnCoin += DisplayEarnCoin;
    }

    private void OnDestroy()
    {
        Informant.OnEarnCoin -= DisplayEarnCoin;
    }

    private void DisplayEarnCoin(int value)
    {
        coinText.SetText(value.ToString("n0"));
    }

    public override void OnSetValues()
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartButtonClicked);
        
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(HomeButtonClicked);
    }


    private void RestartButtonClicked()
    {
        GameManager.instance.RestartCurrentWave();
    }

    private void HomeButtonClicked()
    {
        GameManager.instance.BackHome();
    }
}