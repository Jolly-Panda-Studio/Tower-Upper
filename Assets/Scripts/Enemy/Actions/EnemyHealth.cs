using System;

namespace Lindon.TowerUpper.EnemyUtility.Ability
{
    public class EnemyHealth : BaseAbility
    {
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

        public EnemyHealth(Enemy enemy) : base(enemy)
        {
            var maxHealth = enemy.Data.Health;
            m_Health = new Health(maxHealth);
        }

        public override void OnDestory()
        {

        }

        public void SetHealth(int value) => m_Health.SetHealth(value);

        public void TakeDamage(int value = 1) => m_Health.TakeDamage(value);

        public void ImproveHealth(int value = 1) => m_Health.ImproveHealth(value);

        public void Kill() => m_Health.Kill();
    }
}
