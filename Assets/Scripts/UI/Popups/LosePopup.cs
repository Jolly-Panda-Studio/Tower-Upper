using System;
using JollyPanda.LastFlag.EnemyModule;
using JollyPanda.LastFlag.Handlers;
using UnityEngine;
using UnityEngine.UI;
using MJUtilities.UI;

public class LosePopup : UIPopup
{
    public static LosePopup instance;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    
    public override void OnAwake()
    {
        
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
        GameManager.instance.RestartGame();
    }

    private void HomeButtonClicked()
    {
        //Todo: go to home screen
    }
}