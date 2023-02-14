using Lindon.TowerUpper.GameController;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPage : UIPage
{
    [SerializeField] private TMP_Text m_EnemyScoreText;
    [SerializeField] private Button m_PauseButton;

    protected override void SetValues()
    {
        DisplayEnemyCount();
        Time.timeScale = 1;
    }

    protected override void SetValuesOnSceneLoad()
    {
        EnemyCounter.OnKillEnemy += DisplayEnemyCount;

        m_PauseButton.onClick.RemoveAllListeners();
        m_PauseButton.onClick.AddListener(() =>
        { 
            /*PAUSE PAGE*/
            UserInterfaceManager.OnBackPressed();
        });
    }

    private void OnDestroy()
    {
        EnemyCounter.OnKillEnemy -= DisplayEnemyCount;
    }

    private void DisplayEnemyCount()
    {
        m_EnemyScoreText.SetText($"{EnemyCounter.KilledEnemy}/{EnemyCounter.TotalEnemy}");
    }
}
