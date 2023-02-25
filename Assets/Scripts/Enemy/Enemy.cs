using Lindon.TowerUpper.EnemyUtility.Ability;
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

        [Header("Components")]
        [SerializeField] private EnemyHealthBar m_HealthBar;
        [SerializeField] private Collider m_Collider;

        public EnemyHealth Health { get; private set; }
        public EnemyClimbing Climbing { get; private set; }
        public EnemyFalling Falling { get; private set; }
        public EnemyData Data => m_Data;

        public event Action<Enemy> OnDie
        {
            add => Health.OnDie += () => value?.Invoke(this);
            remove => Health.OnDie -= () => value?.Invoke(this);
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
            Falling = new EnemyFalling(this); 
            Health = new EnemyHealth(this, m_HealthBar);
            Climbing = new EnemyClimbing(this);

            m_AbilityList = new List<BaseAbility>
            {
                Health,
                Climbing,
                Falling
            };
        }

        private void OnEnable()
        {
            GameRunnig.OnChange += OnChangeRunnig;
            GameFinisher.OnFinishGame += GameFinished;
            GameRestarter.OnRestartGame += GameFinished;
            ReturnHome.OnReturnHome += OnReturnHome;
            Health.OnDie += Die;
        }

        private void OnDisable()
        {
            GameRunnig.OnChange -= OnChangeRunnig;
            GameFinisher.OnFinishGame -= GameFinished;
            GameRestarter.OnRestartGame -= GameFinished;
            ReturnHome.OnReturnHome -= OnReturnHome;
            Health.OnDie -= Die;
        }

        private void OnChangeRunnig(bool state)
        {
            Climbing.ChangeClimbingRunnig(state);
        }

        private void GameFinished()
        {
            Climbing.StopClimbing();
        }

        private void OnReturnHome()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            foreach (var ability in m_AbilityList)
            {
                ability.OnDestory();
            }
        }

        private void Die()
        {
            m_Collider.enabled = false;
        }
    }
}