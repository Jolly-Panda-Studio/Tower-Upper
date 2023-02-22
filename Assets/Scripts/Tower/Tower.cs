using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TowerComponents))]
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerComponents m_Components;

    public TowerComponents Components => m_Components;

    public void LoadComponents()
    {
        m_Components ??= gameObject.GetOrAddComponent<TowerComponents>();
        m_Components.LoadComponents();
    }
}