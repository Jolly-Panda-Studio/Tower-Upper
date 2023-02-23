using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Lindon.TowerUpper.EnemyUtility.Ability
{
    public class EnemyClimbing : BaseAbility
    {
        private readonly float m_Speed = 10;

        private Vector3 m_Destination;
        DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_DoClimbing;

        public EnemyClimbing(Enemy enemy) : base(enemy)
        {
            m_Speed = enemy.Data.Speed;

            enemy.OnDie += Die;
        }

        public event Action OnFinishClimb;

        public void SetTargetMove(Transform destination)
        {
            m_Destination = destination.position;
            m_Destination.y -= m_Enemy.GetComponent<CapsuleCollider>().height;
        }

        public void SetLookAt(Transform lookAtTarget)
        {
            m_Enemy.transform.LookAt(lookAtTarget);
        }

        public void Climbimg()
        {
            m_DoClimbing = m_Enemy.transform.DOMoveY(m_Destination.y, GetMoveTime(m_Destination))
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    OnFinishClimb?.Invoke();
                });
        }

        public void StopClimbing()
        {
            if (m_DoClimbing != null)
            {
                m_DoClimbing.Kill();
            }
        }

        public void ChangeClimbingRunnig(bool state)
        {
            if (m_DoClimbing == null) return;

            if (state)
            {
                m_DoClimbing.Play();
            }
            else
            {
                m_DoClimbing.Pause();
            }
        }

        private float GetMoveTime(Vector3 position)
        {
            return Vector3.Distance(m_Enemy.transform.position, position) / m_Speed;
        }

        public override void OnDestory()
        {
            m_Enemy.OnDie -= Die;
        }

        private void Die(Enemy enemy)
        {
            enemy.OnDie -= Die;
            StopClimbing();
        }
    }
}