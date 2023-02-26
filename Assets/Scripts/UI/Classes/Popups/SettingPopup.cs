using Lindon.Framwork.Audio.Controller;
using Lindon.Framwork.Audio.Data;
using Lindon.Framwork.Audio.Event;
using Lindon.UserManager;
using Lindon.UserManager.Base.Page;
using Lindon.UserManager.Tools;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    [SerializeField] private EventTrigger m_trigger;

    [Header("Buttons")]
    [SerializeField] private Button m_CloseButton;
    [SerializeField] private ToggleButton m_MusicToggle;
    [SerializeField] private ToggleButton m_SFXToggle;

    protected override void SetValues()
    {
        m_MusicToggle.IsOn = !AudioMuter.IsMute(AudioSourceType.Music);
        m_SFXToggle.IsOn = !AudioMuter.IsMute(AudioSourceType.SFX);
    }

    protected override void SetValuesOnSceneLoad()
    {
        m_CloseButton.onClick.RemoveAllListeners();
        m_CloseButton.onClick.AddListener(Close);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((data) => OnClick((PointerEventData)data));
        m_trigger.triggers.Add(clickEntry);

        m_MusicToggle.OnChangeValue += MuteMusic;
        m_SFXToggle.OnChangeValue += MuteSFX;
    }

    private void OnDestroy()
    {
        m_MusicToggle.OnChangeValue -= MuteMusic;
        m_SFXToggle.OnChangeValue -= MuteSFX;
    }

    private void OnClick(PointerEventData data)
    {
        Close();
    }

    private void Close()
    {
        UserInterfaceManager.OnBackPressed();
    }

    private void MuteMusic(bool value)
    {
        Debug.Log($"Music {(!value ? "Muted" : "Unmuted")}!");
        AudioMuter.SetMute(AudioSourceType.Music, !value);
    }

    private void MuteSFX(bool value)
    {
        Debug.Log($"SFX {(!value ? "Muted" : "Unmuted")}!");
        AudioMuter.SetMute(AudioSourceType.SFX, !value);
    }
}
