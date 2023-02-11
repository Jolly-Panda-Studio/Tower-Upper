using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.GameController;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lindon.UserManager.Page.Shop
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_CostText;
        [SerializeField] private Button m_Button;

        private ItemData m_SelectedItem;

        public event Action<ItemData> OnBuy;

        private void Start()
        {
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(() =>
            {
                var boughtItem = ShopController.Buy(m_SelectedItem);
                if (boughtItem)
                {
                    OnBuy?.Invoke(m_SelectedItem);
                }
            });
        }

        public void SelectItem(ItemData item)
        {
            m_SelectedItem = item;
            SetCost(item.Cost);
        }

        private void SetCost(float cost)
        {
            m_CostText.SetText($"{cost:n0}");
        }

        public void SetInteractable(bool value)
        {
            m_Button.interactable = value;
        }
    }
}