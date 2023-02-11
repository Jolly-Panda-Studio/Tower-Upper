using Lindon.TowerUpper.Data;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lindon.UserManager.Page.Shop
{
    [RequireComponent(typeof(Toggle))]
    public class ShopSlot : MonoBehaviour
    {
        [SerializeField] private Image m_IconRendrer;
        [SerializeField] private Toggle m_Toggle;

        [SerializeField] private GameObject m_LockObject;

        [Header("Backgrounds")]
        [SerializeField] private Image m_BackgroundRendrer;
        [SerializeField] private Sprite m_LockSprite;
        [SerializeField] private Sprite m_UnlockSprite;
        [SerializeField] private Sprite m_ActiveSprite;

        [Header("Select")]
        [SerializeField] private Image m_SelectedFrame;
        [SerializeField] private Animator m_Animator;
        [SerializeField] private string m_SelectBoolParameter;

        private BuyButton m_BuyButton;

        public event System.Action<bool, ItemData> OnSlotSelectedHandler;


        public int Id { get; private set; } = -1;

        private void Start()
        {
            var toggleGroup = GetComponentInParent<ToggleGroup>();
            if (toggleGroup == null)
            {
                Debug.LogError("A toggle group was not found in the parent object", gameObject);
                return;
            }
            m_Toggle.group = toggleGroup;
        }

        public void SetBuyButton(BuyButton buyButton)
        {
            m_BuyButton = buyButton;
        }

        public void SetItem(ItemData item)
        {
            Id = item.Id;

            m_IconRendrer.sprite = item.Icon;

            SetToggleAction(item);
        }

        private void SetToggleAction(ItemData item)
        {
            m_Toggle.onValueChanged.RemoveAllListeners();
            m_Toggle.onValueChanged.AddListener((value) =>
            {
                OnSlotSelectedHandler?.Invoke(value, item);

                ApplySelectedFrame(value);

                PlaySelectAnimation(value);

                if (value)
                {
                    SetBuyOption(item);
                }
            });
        }

        private void ApplySelectedFrame(bool value) => m_SelectedFrame?.gameObject.SetActive(value);
        private void PlaySelectAnimation(bool value) => m_Animator?.SetBool(m_SelectBoolParameter, value);
        private void SetBuyOption(ItemData item) => m_BuyButton.SelectItem(item);

        public void SetLockSkin()
        {
            m_BuyButton.SetInteractable(true);

            m_BackgroundRendrer.sprite = m_LockSprite;

            m_LockObject.SetActive(true);
        }

        public void SetUnlockSkin()
        {
            m_BuyButton.SetInteractable(false);

            m_BackgroundRendrer.sprite = m_UnlockSprite;

            m_LockObject.SetActive(false);
        }

        public void SetActiveSkin()
        {
            m_BuyButton.SetInteractable(false);

            m_BackgroundRendrer.sprite = m_ActiveSprite;
            
            m_LockObject.SetActive(false);
        }

        public void SelectSlot()
        {
            if (m_Toggle.isOn) return;
            m_Toggle.isOn = true;
        }

        public void SetIsOn(bool value)
        {
            m_Toggle.isOn = value;
        }
    }
}