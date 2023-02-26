using Lindon.TowerUpper.GameController.Events;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Component
{

    public class EnemyAnimation : BaseComponent
    {
        [SerializeField] private Animator m_animator;

        [Header("Parameter")]
        [SerializeField] private string m_fallParameter;
        [SerializeField] private string m_victoryParameter;
        [SerializeField] private string m_idleParameter;

        private bool m_isVictory;
        private float m_speed;

        protected override void OnCreate()
        {
            m_speed = m_Enemy.Data.Speed / 2;
            m_animator.speed = m_speed;
        }

        public override void RegisterEvents()
        {
            m_Enemy.OnDie += DieAnimation;
            m_Enemy.OnFinishClimb += VictoryAnimation;

            GameFinisher.OnFinishGame += DisableAnimator;
            GameRunnig.OnChange += SetAnimatorActive;
        }

        public override void UnregisterEvents()
        {
            m_Enemy.OnDie -= DieAnimation;
            m_Enemy.OnFinishClimb -= VictoryAnimation;

            GameFinisher.OnFinishGame -= DisableAnimator;
            GameRunnig.OnChange -= SetAnimatorActive;
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

        private void DisableAnimator()
        {
            if (m_isVictory) return;
            m_animator.speed = 1;
            m_animator.SetTrigger(m_idleParameter);
        }

        private void SetAnimatorActive(bool value)
        {
            if (value)
            {
                m_animator.speed = m_speed;
            }
            else
            {
                m_animator.speed = 0;
            }
        }
    }
}