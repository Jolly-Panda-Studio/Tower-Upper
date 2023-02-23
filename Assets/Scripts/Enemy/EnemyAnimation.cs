using Lindon.TowerUpper.GameController.Events;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Controller
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private Enemy m_enemy;

        [Header("Parameter")]
        [SerializeField] private string m_fallParameter;
        [SerializeField] private string m_victoryParameter;
        [SerializeField] private string m_idleParameter;

        private bool m_isVictory;

        private void Start()
        {
            m_animator.speed = m_enemy.Data.Speed / 2;
        }

        private void OnEnable()
        {
            m_enemy.OnDie += DieAnimation;
            m_enemy.OnFinishClimb += VictoryAnimation;

            GameFinisher.OnFinishGame += DisableAnimator;
        }

        private void OnDisable()
        {
            m_enemy.OnDie -= DieAnimation;
            m_enemy.OnFinishClimb -= VictoryAnimation;

            GameFinisher.OnFinishGame -= DisableAnimator;
        }

        private void DieAnimation(Enemy obj)
        {
            m_animator.speed = 1;
            m_animator.SetTrigger(m_fallParameter);
        }

        private void VictoryAnimation(Enemy obj)
        {
            m_isVictory = true;
            m_animator.speed = 1;
            m_animator.SetTrigger(m_victoryParameter);
        }

        public void DisableAnimator()
        {
            if (m_isVictory) return;
            m_animator.speed = 1;
            m_animator.SetTrigger(m_idleParameter);
        }
    }
}