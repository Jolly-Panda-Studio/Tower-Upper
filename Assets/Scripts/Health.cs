using System;

public class Health
{
    private int m_Health;
    private int m_MaxHealth;

    public bool IsAlive { get; private set; }

    public int CurrrentHealth
    {
        get => m_Health;
        set
        {
            m_Health = value;
            if (m_Health <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke();
            }
            else
            {
                IsAlive = true;
            }
        }
    }

    public Health(int health)
    {
        CurrrentHealth = health;
        m_MaxHealth = health;
    }

    public event Action<int, int> OnHealthChange;
    public event Action OnDie;

    public void SetHealth(int value)
    {
        CurrrentHealth = value;
        m_MaxHealth = value;
    }

    public void TakeDamage(int value = 1)
    {
        if (!IsAlive) return;
        CurrrentHealth -= value;
        OnHealthChange?.Invoke(CurrrentHealth, m_MaxHealth);
    }

    public void ImproveHealth(int value = 1)
    {
        if (!IsAlive) return;
        CurrrentHealth += value;
        OnHealthChange?.Invoke(CurrrentHealth, m_MaxHealth);
    }

    public void Kill()
    {
        if (!IsAlive) return;
        CurrrentHealth = 0;
        OnHealthChange?.Invoke(CurrrentHealth, m_MaxHealth);
    }
}
