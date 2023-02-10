using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Switch : MonoBehaviour
{
    [SerializeField] private Image toggleImage;
    [SerializeField] private Image holderImage;

    [SerializeField] private Skin onSkin;
    [SerializeField] private Skin offSkin;
    [Space]
    private GameObject switchBtn;

    Button _button;

    public Action<bool> onSwitch;

    public bool IsOn
    {
        get => isOn; 
        set
        {
            isOn = value;
            ChangeSkin(isOn);
            onSwitch?.Invoke(value);
        }
    }
    float onPosX;
    float offPosX;
    [SerializeField] private bool isOn = false;

    private void Start()
    {
        switchBtn = holderImage.gameObject;

        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            //DoSwitch();
            OnSwitchButtonClicked();
        });

        onPosX = -switchBtn.transform.localPosition.x;
        offPosX = switchBtn.transform.localPosition.x;
    }

    private void OnEnable()
    {
    }

    private void SetOn()
    {
        if (IsOn == true) return;
        switchBtn.transform.DOLocalMoveX(onPosX, 0.2f);
        IsOn = true;
    }

    private void SetOff()
    {
        if (IsOn == false) return;
        switchBtn.transform.DOLocalMoveX(offPosX, 0.2f);
        IsOn = false;
    }

    private void OnSwitchButtonClicked()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, 0.2f);
        IsOn = !IsOn;
    }

    private void ChangeSkin(bool state)
    {
        if (state)
        {
            ChangeSkin(onSkin);
        }
        else
        {
            ChangeSkin(offSkin);
        }
    }

    private void ChangeSkin(Skin skin)
    {
        toggleImage.sprite = skin.toggle;
        holderImage.sprite = skin.holder;
    }

    [Serializable]
    private struct Skin
    {
        public Sprite toggle;
        public Sprite holder;
    }
}
