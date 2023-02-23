using System;

namespace Lindon.TowerUpper.EnemyUtility.Ability
{
    public abstract class BaseAbility
    {
        protected readonly Enemy m_Enemy;

        protected BaseAbility(Enemy enemy)
        {
            m_Enemy = enemy;
        }
    }
}