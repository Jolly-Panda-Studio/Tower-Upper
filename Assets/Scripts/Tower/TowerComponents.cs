using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpawnPointCreator))]
public class TowerComponents : MonoBehaviour
{
    [SerializeField] private SpawnPointCreator m_SpawnPointCreator;
    [SerializeField] private Transform m_Floor;

    public SpawnPointCreator SpawnPointCreator => m_SpawnPointCreator;
    public Transform Floor => m_Floor;

    public void LoadComponents()
    {
        m_SpawnPointCreator ??= gameObject.GetOrAddComponent<SpawnPointCreator>();
    }
}
