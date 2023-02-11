using Lindon.UserManager.Base.Page;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPage : UIPage
{
    [SerializeField] private TMP_Text m_EnemyScoreText;
    [SerializeField] private Button m_PauseButton;

    protected override void SetValues()
    {
        m_EnemyScoreText.SetText($"{0}/{EnemyCounter.TotalEnemy}");
        Time.timeScale = 1;
    }

    protected override void SetValuesOnSceneLoad()
    {
        EnemyCounter.OnKillEnemy += DisplayEnemyCount;

        m_PauseButton.onClick.RemoveAllListeners();
        m_PauseButton.onClick.AddListener(() => { /*PAUSE PAGE*/ });
    }

    private void OnDestroy()
    {
        EnemyCounter.OnKillEnemy -= DisplayEnemyCount;
    }

    private void DisplayEnemyCount(int killedEnemy)
    {
        m_EnemyScoreText.SetText($"{killedEnemy}/{EnemyCounter.TotalEnemy}");
    }
}
