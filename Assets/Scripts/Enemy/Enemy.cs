using JollyPanda.LastFlag.Handlers;
using System;
using UnityEngine;

namespace JollyPanda.LastFlag.EnemyModule
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyPositionChecker positionChecker;

        private Action<Enemy> OnDead;

        private void OnEnable()
        {
            health.OnHealthChange += OnHealthChange;
            movement.OnStartClimbing += Climbing;
            movement.OnStartFalling += Falling;
            positionChecker.OnEnemyReachedTop += ReachingTop;
            positionChecker.OnEnemyReachedGround += ReachingGround;
            Informant.OnEnemyReachedTop += OneEnemyReachedTop;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= OnHealthChange;
            movement.OnStartClimbing -= Climbing;
            movement.OnStartFalling -= Falling;
            positionChecker.OnEnemyReachedTop -= ReachingTop;
            positionChecker.OnEnemyReachedGround -= ReachingGround;
            Informant.OnEnemyReachedTop -= OneEnemyReachedTop;
        }

        private void OnHealthChange(int healthValue)
        {
            if (healthValue <= 0)
            {
                movement.Falling();
                OnDead?.Invoke(this);
            }
            else
            {
                movement.Climbing();
            }
        }

        private void Climbing()
        {
            animationController.PlayClimb();
            positionChecker.SetCheckState(EnemyPositionChecker.CheckState.MoveUp);
        }

        private void Falling()
        {
            animationController.PlayFall();
            positionChecker.SetCheckState(EnemyPositionChecker.CheckState.MoveDown);
            health.ShowHealthBar(false);
        }

        private void ReachingTop()
        {
            movement.StopClimbing();
            health.ShowHealthBar(false);
            animationController.PlayClimbingOverEdge();
            Informant.NotifyEnemyReachedTop(this);
        }

        private void OneEnemyReachedTop(Enemy reachedEnemy)
        {
            if (reachedEnemy == this)
                return;

            movement.StopClimbing();
            animationController.PlayHangingIdle();
            health.ShowHealthBar(false);
        }

        private void ReachingGround()
        {
            gameObject.SetActive(false);
        }

        public void SetClimbSpeed(float value)
        {
            movement.SetClimbSpeed(value);
        }

        public void SetDeadAction(Action<Enemy> action)
        {
            OnDead = action;
        }
    }
}
