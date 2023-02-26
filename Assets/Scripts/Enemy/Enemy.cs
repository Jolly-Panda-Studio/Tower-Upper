using Lindon.TowerUpper.EnemyUtility.Ability;
using Lindon.TowerUpper.EnemyUtility.Component;
using Lindon.TowerUpper.GameController.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField, AssetPopup(typeof(EnemyData))] private EnemyData m_Data;

        private List<BaseAbility> m_AbilityList;
        private bool m_Created;

        [Header("Components")]
        [SerializeField] private EnemyHealthBar m_HealthBar;
        [SerializeField] private EnemyVoice m_Voice;
        [SerializeField] private EnemyCollider m_Collider;
        [SerializeField] private EnemyAnimation m_Animation;
        [SerializeField] private EnemyEvent m_Event;

        private List<BaseComponent> m_Components;

        public EnemyHealth Health { get; private set; }
        public EnemyClimbing Climbing { get; private set; }
        public EnemyFalling Falling { get; private set; }
        public EnemyData Data => m_Data;

        public event Action<Enemy> OnDie
        {
            add => Health.OnDie += () => value?.Invoke(this);
            remove => Health.OnDie -= () => value?.Invoke(this);
        }

        public event Action<Enemy> OnStartClimbing
        {
            add => Climbing.OnStart += () => value?.Invoke(this);
            remove => Climbing.OnStart -= () => value?.Invoke(this);
        }

        public event Action<Enemy> OnFinishClimb
        {
            add => Climbing.OnFinishClimb += () => value?.Invoke(this);
            remove => Climbing.OnFinishClimb -= () => value?.Invoke(this);
        }

        public event Action<Enemy> OnFalling
        {
            add => Falling.OnFalling += () => value?.Invoke(this);
            remove => Falling.OnFalling -= () => value?.Invoke(this);
        }

        private void Awake()
        {
            CreateComponents();

            CreateAbilities();
        }

        private void CreateComponents()
        {
            m_Components = new List<BaseComponent>
            {
                m_HealthBar,
                m_Voice,
                m_Collider,
                m_Animation,
                m_Event
            };

            foreach (var component in m_Components)
            {
                component.Create(this);
            }
        }

        private void OnEnable()
        {
            foreach (var component in m_Components)
            {
                component.RegisterEvents();
            }
        }

        private void OnDisable()
        {
            foreach (var component in m_Components)
            {
                component.UnregisterEvents();
            }
        }

        private void CreateAbilities()
        {
            if (m_Created) return;
            m_Created = true;

            Falling = new EnemyFalling(this);
            Health = new EnemyHealth(this);
            Climbing = new EnemyClimbing(this);

            m_AbilityList = new List<BaseAbility>
            {
                Health,
                Climbing,
                Falling
            };
        }

        private void OnDestroy()
        {
            foreach (var ability in m_AbilityList)
            {
                ability.OnDestory();
            }
        }
    }
}