using Lindon.TowerUpper.GameController;
using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Manager.Enemies;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDPage : UIPage
{
    [SerializeField] private TMP_Text m_EnemyScoreText;
    [SerializeField] private Button m_PauseButton;

    [Header("Rotate")]
    [SerializeField] private EventTrigger trigger;
    [SerializeField, Min(0)] private float rotateSpeed = 1;
    private Transform roatetTransform;
    private float _rotationVelocity;
    private bool m_ActiveDrag;

    protected override void SetValues()
    {
        m_ActiveDrag = true;
        DisplayEnemyCount(0, EnemyCounter.TotalEnemy);
        Time.timeScale = 1;
        roatetTransform = GameManager.Instance.Tower.transform;
    }

    protected override void SetValuesOnSceneLoad()
    {
        EnemyCounter.OnKillEnemy += DisplayEnemyCount;

        m_PauseButton.onClick.RemoveAllListeners();
        m_PauseButton.onClick.AddListener(() =>
        {
            UserInterfaceManager.Open<PausePopup>();
        });

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
        trigger.triggers.Add(dragEntry);
    }

    private void OnEnable()
    {
        GameRestarter.OnRestartGame += RestartGame;
    }

    private void OnDisable()
    {
        GameRestarter.OnRestartGame -= RestartGame;
        m_ActiveDrag = false;
    }

    private void OnDestroy()
    {
        EnemyCounter.OnKillEnemy -= DisplayEnemyCount;
    }

    private void RestartGame()
    {
        DisplayEnemyCount(0, EnemyCounter.TotalEnemy);
    }

    private void DisplayEnemyCount(int killedCount, int totalCount)
    {
        m_EnemyScoreText.SetText($"{killedCount}/{totalCount}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!m_ActiveDrag) return;

        _rotationVelocity = eventData.delta.x * rotateSpeed;
        roatetTransform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);
    }
}
