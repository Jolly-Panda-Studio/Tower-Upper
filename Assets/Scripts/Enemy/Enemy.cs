using JollyPanda.LastFlag.Handlers;
using UnityEngine;

namespace JollyPanda.LastFlag.EnemyModule
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyPositionChecker positionChecker;


        private void OnEnable()
        {
            health.OnHealthChange += OnHealthChange;
            movement.OnStartClimbing += Climbing;
            movement.OnStartFalling += Falling;
            positionChecker.OnEnemyReachedTop += ReachingTop;
            Informant.OnEnemyReachedTop += OneEnemyReachedTop;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= OnHealthChange;
            movement.OnStartClimbing -= Climbing;
            movement.OnStartFalling -= Falling;
            positionChecker.OnEnemyReachedTop -= ReachingTop;
            Informant.OnEnemyReachedTop -= OneEnemyReachedTop;
        }

        private void OnHealthChange(int healthValue)
        {
            if (healthValue <= 0)
            {
                movement.Falling();
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
    }
}
