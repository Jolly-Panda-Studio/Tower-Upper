using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lindon.UserManager.Tools
{
    [RequireComponent(typeof(Button))]
    public class OpenLinkerButton : MonoBehaviour
    {
        [SerializeField] private string m_URL;
        private Button m_button;

        private void Start()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.RemoveAllListeners();
            m_button.onClick.AddListener(() =>
            {
                Application.OpenURL(m_URL);
            });
        }
    }
}