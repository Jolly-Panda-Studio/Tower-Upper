using UnityEngine;
using UnityEngine.UI;

namespace Lindon.TowerUpper.EnemyUtility.Component
{
    public class EnemyHealthBar : BaseComponent
    {
        [SerializeField] private Slider m_HealthSlider;

        private Camera m_Camera;

        protected override void OnCreate()
        {
            m_Camera = Camera.main;
            SetMaxValue(m_Enemy.Data.Health);
        }

        public override void RegisterEvents()
        {
            m_Enemy.Health.OnHealthChange += HealthChange;
            m_Enemy.OnDie += Die;

            m_Enemy.OnFalling += FallEnemy;
        }

        public override void UnregisterEvents()
        {
            m_Enemy.Health.OnHealthChange -= HealthChange;
            m_Enemy.OnDie -= Die;

            m_Enemy.OnFalling -= FallEnemy;
        }

        private void HealthChange(int currentHealth, int maxHealth)
        {
            if (currentHealth != maxHealth)
            {
                SetActive(true);
            }
            m_HealthSlider.value = currentHealth;
        }

        private void Die(Enemy enemy)
        {
            SetActive(false);
        }

        private void FallEnemy(Enemy enemy)
        {
            SetActive(false);
        }

        private void SetMaxValue(int value)
        {
            m_HealthSlider.maxValue = value;
            m_HealthSlider.value = value;
        }

        private void SetActive(bool value)
        {
            m_HealthSlider.gameObject.SetActive(value);
        }

        private void LateUpdate()
        {
            transform.forward = -m_Camera.transform.forward;
        }
    }
}
