using Lindon.UserManager.Base.Page;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPage : UIPage
{
    [SerializeField] private TMP_Text m_EnemyScoreText;

    protected override void SetValues()
    {
        m_EnemyScoreText.SetText("0");
    }

    protected override void SetValuesOnSceneLoad()
    {
        EnemyCounter.OnKillEnemy += DisplayEnemyCount;
    }

    private void OnDestroy()
    {
        EnemyCounter.OnKillEnemy -= DisplayEnemyCount;
    }

    private void DisplayEnemyCount(int killedEnemy)
    {
        m_EnemyScoreText.SetText(killedEnemy.ToString());
    }
}
