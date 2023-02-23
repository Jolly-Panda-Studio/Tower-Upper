using Lindon.TowerUpper.EnemyUtility.Ability;
using Lindon.TowerUpper.GameController.Events;
using System;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField, AssetPopup(typeof(EnemyData))] private EnemyData m_Data;

        public Health Health { get; private set; }
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
            Health = new Health(Data.Health);
            Climbing = new EnemyClimbing(this, Data.Speed);
            Falling = new EnemyFalling(this);
        }

        private void OnEnable()
        {
            GameRunnig.OnChange += OnChangeRunnig;
            GameFinisher.OnFinishGame += GameFinished;
            GameRestarter.OnRestartGame += GameFinished;
            ReturnHome.OnReturnHome += OnReturnHome;
        }

        private void OnDisable()
        {
            GameRunnig.OnChange -= OnChangeRunnig;
            GameFinisher.OnFinishGame -= GameFinished;
            GameRestarter.OnRestartGame -= GameFinished;
            ReturnHome.OnReturnHome -= OnReturnHome;
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
    }
}