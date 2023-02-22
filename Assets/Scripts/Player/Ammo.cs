using Lindon.TowerUpper.GameController.Events;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnEnable()
    {
        GameRunnig.OnChange += OnChangeRunnig;
        GameFinisher.OnFinishGame += GameFinished;
        GameRestarter.OnRestartGame += GameFinished;
    }

    private void OnDisable()
    {
        GameRunnig.OnChange -= OnChangeRunnig;
        GameFinisher.OnFinishGame -= GameFinished;
        GameRestarter.OnRestartGame -= GameFinished;
    }

    private void OnChangeRunnig(bool state)
    {
        if (state)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void GameFinished()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Destroy(gameObject);
            enemy.Kill();
            enemy.FallDown(force);
        }
    }
}
