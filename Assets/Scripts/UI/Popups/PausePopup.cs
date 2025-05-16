using System;
using UnityEngine;
using UnityEngine.UI;
using MJUtilities.UI;

public class PausePopup : UIPopup
{
    [Header("Buttons")]
    [SerializeField] private Button quitButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    public override void OnSetValues()
    {
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitButtonClicked);
        
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(ResumeButtonClicked);
        
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartButtonClicked);

    }
    
    public override void OnAwake()
    {
        
    }
    
    private void QuitButtonClicked()
    {
        GameManager.instance.UnPauseGame();
        GameManager.instance.BackHome();
        UIManager.instance.OpenPage(PageType.Home);
    }
    private void ResumeButtonClicked()
    {
        GameManager.instance.UnPauseGame();
        UIManager.instance.ClosePopup(PopupType.Pause);
    }
    private void RestartButtonClicked()
    {
        //GameManager.instance.UnPauseGame();
        //GameManager.instance.BackHome();
        //UIManager.instance.OpenPage(PageType.Home);
    }
}