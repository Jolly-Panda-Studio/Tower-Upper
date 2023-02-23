using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Lindon/TowerUpper/Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float m_Speed;
    [SerializeField] private int m_Health;

    public int Health { get => m_Health; set => m_Health = value; }
    public float Speed { get => m_Speed; set => m_Speed = value; }
}
