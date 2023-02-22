using Lindon.TowerUpper.GameController.Events;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : UIPopup
{
    [Header("Buttons")]
    [SerializeField] private Button m_SettingButton;
    [SerializeField] private Button m_ContinueButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_GiveUpButton;

    protected override void SetValues()
    {
        GameRunnig.IsRunning = false;
    }

    protected override void SetValuesOnSceneLoad()
    {
        m_SettingButton.onClick.RemoveAllListeners();
        m_SettingButton.onClick.AddListener(OnSetting);

        m_ContinueButton.onClick.RemoveAllListeners();
        m_ContinueButton.onClick.AddListener(OnContinue);

        m_RestartButton.onClick.RemoveAllListeners();
        m_RestartButton.onClick.AddListener(OnRestart);

        m_GiveUpButton.onClick.RemoveAllListeners();
        m_GiveUpButton.onClick.AddListener(OnGiveUp);
    }

    private void OnSetting()
    {
        Debug.Log("Not yet!!");
    }

    private void OnContinue()
    {
        GameRunnig.IsRunning = true;
        UserInterfaceManager.OnBackPressed();
    }

    private void OnRestart()
    {
        GameRunnig.IsRunning = true;
        GameRestarter.RestartGame();
        UserInterfaceManager.OnBackPressed();
    }

    private void OnGiveUp()
    {
        GameFinisher.FinishGame();
        UserInterfaceManager.OnBackPressed();
        UserInterfaceManager.Open<HomePage>();
    }
}
