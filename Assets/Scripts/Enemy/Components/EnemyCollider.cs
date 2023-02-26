using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Component
{
    [RequireComponent(typeof(Collider))]
    public class EnemyCollider : BaseComponent
    {
        private Collider m_Collider;

        protected override void OnCreate()
        {
            m_Collider = GetComponent<Collider>();
        }

        public override void RegisterEvents()
        {
            m_Enemy.OnDie += DiactiveCollider;
        }

        public override void UnregisterEvents()
        {
            m_Enemy.OnDie -= DiactiveCollider;
        }

        private void DiactiveCollider(Enemy enemy)
        {
            m_Collider.enabled = false;
        }
    }
}