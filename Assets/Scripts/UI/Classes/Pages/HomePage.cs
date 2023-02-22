using Lindon.TowerUpper.GameController.Events;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using Lindon.UserManager.Page.Home;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomePage : UIPage
{
    [SerializeField] private EventTrigger m_Trigger;

    [Header("Button")]
    [SerializeField] private Button m_ShopButton;
    [SerializeField] private Button m_SettingButton;

    [Space]
    [SerializeField] private StageInfo m_StageInfo;

    protected override void SetValues()
    {
        ReturnHome.Return();
    }

    protected override void SetValuesOnSceneLoad()
    {
        m_ShopButton.onClick.RemoveAllListeners();
        m_ShopButton.onClick.AddListener(() => { UserInterfaceManager.Open<ShopPage>(); });

        m_SettingButton.onClick.RemoveAllListeners();
        m_SettingButton.onClick.AddListener(() => { /*SETTING PAGE*/ });

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        m_Trigger.triggers.Add(entry);
    }

    private void OnPointerDownDelegate(PointerEventData data)
    {
        GameStarter.StartGame();
    }
}
