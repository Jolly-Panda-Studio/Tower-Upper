using DG.Tweening;
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lindon.UserManager.Tools
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [Space]
        [SerializeField] private Image m_toggleImage;
        [SerializeField] private Image m_holderImage;
        [SerializeField] private Image m_iconImage;

        [SerializeField] private Skin m_onSkin;
        [SerializeField] private Skin m_offSkin;
        [Space]
        [SerializeField, ReadOnly] private bool isOn = false;

        private GameObject SwitchBtn => m_holderImage.gameObject;
        [Space]
        [SerializeField] private float m_onPosX;
        [SerializeField] private float m_offPosX;

        public Action<bool> OnChangeValue;

        public bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                ChangeToggle(isOn);
            }
        }

        private void Start()
        {
            m_button ??= GetComponent<Button>();
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() =>
            {
                OnSwitchButtonClicked();
            });
        }

        private void OnSwitchButtonClicked()
        {
            IsOn = !IsOn;
        }

        private void ChangeToggle(bool value)
        {
            if (value)
            {
                ChangeSkin(m_onSkin);
                SwitchBtn.transform.DOLocalMoveX(m_onPosX, 0.2f);
            }
            else
            {
                ChangeSkin(m_offSkin);
                SwitchBtn.transform.DOLocalMoveX(m_offPosX, 0.2f);
            }

            OnChangeValue?.Invoke(value);
        }

        private void ChangeSkin(Skin skin)
        {
            m_toggleImage.sprite = skin.toggle;
            m_holderImage.sprite = skin.holder;
            if (m_iconImage != null)
            {
                m_iconImage.sprite = skin.icon;
            }
        }

        [Serializable]
        private struct Skin
        {
            public Sprite toggle;
            public Sprite holder;
            public Sprite icon;
        }
    }
}