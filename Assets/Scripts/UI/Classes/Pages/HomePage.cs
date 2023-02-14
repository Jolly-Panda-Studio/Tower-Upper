using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
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

    protected override void SetValues()
    {
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
        //UserInterfaceManager.Open<HUDPage>();
        GameStarter.StartGame();
    }
}
