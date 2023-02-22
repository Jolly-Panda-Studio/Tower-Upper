using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private Enemy m_enemy;

    [Header("Parameter")]
    [SerializeField] private string m_fallParameter;
    [SerializeField] private string m_victoryParameter;

    private void OnEnable()
    {
        m_enemy.OnDie += DieAnimation;
        m_enemy.OnFinishClimb += VictoryAnimation;
    }

    private void OnDisable()
    {
        m_enemy.OnDie -= DieAnimation;
        m_enemy.OnFinishClimb -= VictoryAnimation;
    }

    private void DieAnimation(Enemy obj)
    {
        m_animator.SetTrigger(m_fallParameter);
    }

    private void VictoryAnimation(Enemy obj)
    {
        m_animator.SetTrigger(m_victoryParameter);
    }
}
