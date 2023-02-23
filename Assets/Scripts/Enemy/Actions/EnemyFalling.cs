using System;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Ability
{
    public class EnemyFalling : BaseAbility
    {
        public event Action OnFalling;

        public EnemyFalling(Enemy enemy) : base(enemy)
        {
        }

        public void FallDown(float force)
        {
            var rigidbody = m_Enemy.gameObject.GetOrAddComponent<Rigidbody>();
            var forceVector = -(m_Enemy.transform.forward / 4) + Vector3.down;
            rigidbody.AddForce(forceVector * force, ForceMode.Impulse);

            OnFalling?.Invoke();
        }
    }
}