using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TowerComponents))]
public class Tower : MonoBehaviour
{
    public static Tower Instance { get; private set; }

    [SerializeField] private TowerComponents m_Components;

    public TowerComponents Components => m_Components;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        m_Components ??= gameObject.GetOrAddComponent<TowerComponents>();
    }
}