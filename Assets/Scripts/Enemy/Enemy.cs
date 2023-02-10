using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    private Vector3 m_Destination;
    [SerializeField] private float m_Speed = 10;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> doClimbing;

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
        doClimbing = transform.DOMoveY(m_Destination.y, GetMoveTime(m_Destination));
    }

    public float GetMoveTime(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) / m_Speed;
    }

    public void Kill()
    {
        if(doClimbing != null)
        {
            doClimbing.Kill();

            //fall animation
        }
    }
}