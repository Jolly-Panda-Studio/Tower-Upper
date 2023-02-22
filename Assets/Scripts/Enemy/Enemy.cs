using UnityEngine;
using DG.Tweening;
using System;
using Lindon.TowerUpper.GameController.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_Speed = 10;
    private Vector3 m_Destination;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_DoClimbing;

    public event Action<Enemy> OnDie;
    public event Action<Enemy> OnFinishClimb;

    private void OnEnable()
    {
        GameRunnig.OnChange += OnChangeRunnig;
    }

    private void OnDisable()
    {
        GameRunnig.OnChange -= OnChangeRunnig;
    }

    private void OnChangeRunnig(bool state)
    {
        if (m_DoClimbing != null)
        {
            if (state)
            {
                m_DoClimbing.Play();
            }
            else
            {
                m_DoClimbing.Pause();
            }
        }
    }

    public Enemy SetTargetMove(Transform destination)
    {
        m_Destination = destination.position;
        return this;
    }

    public Enemy SetLookAt(Transform lookAtTarget)
    {
        transform.LookAt(lookAtTarget);
        return this;
    }

    public void Climb()
    {
        m_DoClimbing = transform.DOMoveY(m_Destination.y, GetMoveTime(m_Destination)).OnComplete(() =>
        {
            OnFinishClimb?.Invoke(this);
        });
    }

    public float GetMoveTime(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) / m_Speed;
    }

    public void Kill()
    {
        if(m_DoClimbing != null)
        {
            m_DoClimbing.Kill();

            //fall animation

            OnDie?.Invoke(this);
        }
    }

    public void FallDown(float force)
    {
        var rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.down * force, ForceMode.Impulse);
    }
}