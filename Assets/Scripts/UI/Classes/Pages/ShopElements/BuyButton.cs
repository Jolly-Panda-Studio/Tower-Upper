using System.Collections;
using System.Collections.Generic;
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

        private void Start()
        {
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(() =>
            {

            });
        }

        public void SetCost(float cost)
        {
            m_CostText.SetText(cost.ToString());
        }

        public void ChoiceItem()
        {

        }
    }
}