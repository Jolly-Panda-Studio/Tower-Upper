using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Component
{
    public abstract class BaseComponent : MonoBehaviour
    {
        [SerializeField,ReadOnly] protected Enemy m_Enemy;

        public void Create(Enemy enemy)
        {
            m_Enemy = enemy;
            OnCreate();
        }

        protected abstract void OnCreate();

        public abstract void RegisterEvents();
        public abstract void UnregisterEvents();
    }
}