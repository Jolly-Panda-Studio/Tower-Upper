using System;

namespace Lindon.TowerUpper.EnemyUtility.Ability
{
    public class EnemyHealth : BaseAbility
    {
        private readonly EnemyHealthBar m_HealthSlider;
        private readonly Health m_Health;

        public event Action<int, int> OnHealthChange
        {
            add => m_Health.OnHealthChange += value;
            remove => m_Health.OnHealthChange -= value;
        }

        public event Action OnDie
        {
            add => m_Health.OnDie += value;
            remove => m_Health.OnDie -= value;
        }

        public EnemyHealth(Enemy enemy, EnemyHealthBar healthSlider) : base(enemy)
        {
            var maxHealth = enemy.Data.Health;
            m_Health = new Health(maxHealth);

            m_HealthSlider = healthSlider;
            m_HealthSlider.SetMaxValue(maxHealth);

            m_Health.OnHealthChange += HealthChange;
            m_Health.OnDie += Die;

            m_Enemy.OnFalling += FallEnemy;
        }

        public override void OnDestory()
        {
            m_Health.OnHealthChange -= HealthChange;
            m_Health.OnDie -= Die;
            m_Enemy.OnFalling -= FallEnemy;
        }

        private void HealthChange(int currentHealth, int maxHealth)
        {
            if (currentHealth != maxHealth)
            {
                m_HealthSlider.SetActive(true);
            }
            m_HealthSlider.SetValue(currentHealth);
        }

        private void Die()
        {
            m_HealthSlider.SetActive(false);
        }

        private void FallEnemy(Enemy obj)
        {
            m_HealthSlider.SetActive(false);
        }

        public void SetHealth(int value) => m_Health.SetHealth(value);

        public void TakeDamage(int value = 1) => m_Health.TakeDamage(value);

        public void ImproveHealth(int value = 1) => m_Health.ImproveHealth(value);

        public void Kill() => m_Health.Kill();
    }
}
