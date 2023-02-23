using UnityEngine;
using UnityEngine.UI;

namespace Lindon.TowerUpper.EnemyUtility
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider m_HealthSlider;

        private Camera m_Camera;

        private void Start()
        {
            m_Camera = Camera.main;
        }

        public void SetMaxValue(int value)
        {
            m_HealthSlider.maxValue = value;
            m_HealthSlider.value = value;
        }

        public void SetValue(int value)
        {
            m_HealthSlider.value = value;
        }

        public void SetActive(bool value)
        {
            m_HealthSlider.gameObject.SetActive(value);
        }

        private void LateUpdate()
        {
            transform.forward = -m_Camera.transform.forward;
        }
    }
}
