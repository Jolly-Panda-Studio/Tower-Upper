using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JollyPanda.LastFlag.EnemyModule;
using JollyPanda.LastFlag.Handlers;
using UnityEngine;
using UnityEngine.UI;
using MJUtilities.UI;
using TMPro;

public class HUDPage : UIPage
{
    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text enemyStatusText;
    [SerializeField] private TMP_Text startWaveText;
    [SerializeField] private TMP_Text waveIndexText;
    
    [Header("CanvasGroups")]
    [SerializeField] private CanvasGroup newWaveNotificationCanvasGroup;

    private void OnEnable()
    {
        Informant.OnEnemyKilled += EnemyKilled;
        Informant.OnWaveEnd += WaveEnd;
        Informant.OnWaveStart += WaveStart;
        Informant.OnEnemyReachedTop += EnemyReachedTop;
    }
    private void OnDisable()
    {
        Informant.OnEnemyKilled -= EnemyKilled;
        Informant.OnWaveEnd -= WaveEnd;
        Informant.OnWaveStart -= WaveStart;
        Informant.OnEnemyReachedTop -= EnemyReachedTop;
    }

    public override void OnSetValues()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(PauseButtonClicked);
        
    }

    private void Start()
    {
        newWaveNotificationCanvasGroup.alpha = 0;
    }

    public override void OnAwake()
    {
        
    }

    private void PauseButtonClicked()
    {
        UIManager.instance.OpenPopup(PopupType.Pause);
        GameManager.instance.PauseGame();
    }
    
    private void EnemyKilled(int alivedEnemyCount, int totalEnemy)
    {
        enemyStatusText.text = alivedEnemyCount + "/" + totalEnemy;
    }
    private void WaveEnd(int waveIndex, int killedEnemy)
    {
        UIManager.instance.OpenPopup(PopupType.Countdown);
        GameManager.instance.PauseGame();
    }
    private void WaveStart(int waveIndex)
    {
        GameManager.instance.UnPauseGame();
        
        var currentWave = waveIndex + 1;
        waveIndexText.text = currentWave.ToString();
        StartCoroutine(WaveNotificationCoroutine(currentWave));
    }
    
    private void EnemyReachedTop(Enemy enemy)
    {
        UIManager.instance.OpenPopup(PopupType.Lose);
    }

    private IEnumerator WaveNotificationCoroutine(int waveIndex)
    {
        newWaveNotificationCanvasGroup.alpha = 1f;
        startWaveText.text = $"Enemies rise!\n Wave {waveIndex} has begun!";
        yield return new WaitForSecondsRealtime(5f);
        newWaveNotificationCanvasGroup.alpha = 0f;
    }
    
    
    

    [ContextMenu(nameof(TestOnEnemyKilled_Call))]
    public void TestOnEnemyKilled_Call()
    {
        Informant.NotifyEnemyKilled(5,15);
    }
    [ContextMenu(nameof(TestWaveStart_Call))]
    public void TestWaveStart_Call()
    {
        Informant.NotifyStartWave(2);
    }
    [ContextMenu(nameof(TestOnWaveEnd_Call))]
    public void TestOnWaveEnd_Call()
    {
        Informant.NotifyFinishWave(1,15);
    }
}