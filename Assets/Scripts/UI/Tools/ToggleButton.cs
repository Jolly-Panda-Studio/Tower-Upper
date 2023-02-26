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

        [SerializeField] private Skin m_onSkin;
        [SerializeField] private Skin m_offSkin;
        [Space]
        [SerializeField, ReadOnly] private bool isOn = false;

        private GameObject m_switchBtn;

        private float m_onPosX;
        private float m_offPosX;

        public Action<bool> OnChangeValue;

        public bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                ChangeSkin(isOn);
                OnChangeValue?.Invoke(value);
            }
        }

        private void Start()
        {
            m_switchBtn = m_holderImage.gameObject;

            m_button ??= GetComponent<Button>();
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() =>
            {
                OnSwitchButtonClicked();
            });

            m_onPosX = -m_switchBtn.transform.localPosition.x;
            m_offPosX = m_switchBtn.transform.localPosition.x;
        }

        private void SetOn()
        {
            if (IsOn == true) return;
            m_switchBtn.transform.DOLocalMoveX(m_onPosX, 0.2f);
            IsOn = true;
        }

        private void SetOff()
        {
            if (IsOn == false) return;
            m_switchBtn.transform.DOLocalMoveX(m_offPosX, 0.2f);
            IsOn = false;
        }

        private void OnSwitchButtonClicked()
        {
            m_switchBtn.transform.DOLocalMoveX(-m_switchBtn.transform.localPosition.x, 0.2f);
            IsOn = !IsOn;
        }

        private void ChangeSkin(bool state)
        {
            if (state)
            {
                ChangeSkin(m_onSkin);
            }
            else
            {
                ChangeSkin(m_offSkin);
            }
        }

        private void ChangeSkin(Skin skin)
        {
            m_toggleImage.sprite = skin.toggle;
            m_holderImage.sprite = skin.holder;
        }

        [Serializable]
        private struct Skin
        {
            public Sprite toggle;
            public Sprite holder;
        }
    }
}