using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    private Vector3 m_Destination;

    public Enemy SetTargetMove(Transform destination)
    {
        m_Destination = destination.position;
        return this;
    }

    public void Climb()
    {
        transform.DOMoveY(m_Destination.y, 2);
    } 
}