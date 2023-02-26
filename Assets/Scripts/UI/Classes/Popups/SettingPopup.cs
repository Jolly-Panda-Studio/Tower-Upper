using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    [SerializeField] private EventTrigger m_trigger;

    [Header("Buttons")]
    [SerializeField] private Button m_CloseButton;

    protected override void SetValues()
    {

    }

    protected override void SetValuesOnSceneLoad()
    {
        m_CloseButton.onClick.RemoveAllListeners();
        m_CloseButton.onClick.AddListener(Close);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((data) => OnClick((PointerEventData)data));
        m_trigger.triggers.Add(clickEntry);
    }

    private void OnClick(PointerEventData data)
    {
        Close();
    }

    private void Close()
    {
        UserInterfaceManager.OnBackPressed();
    }
}
