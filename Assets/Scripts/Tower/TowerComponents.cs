using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpawnPointCreator))]
public class TowerComponents : MonoBehaviour
{
    [SerializeField] private SpawnPointCreator m_SpawnPointCreator;

    public SpawnPointCreator SpawnPointCreator => m_SpawnPointCreator;

    public void LoadComponents()
    {
        m_SpawnPointCreator ??= gameObject.GetOrAddComponent<SpawnPointCreator>();
    }
}
