using Lindon.TowerUpper.EnemyUtility;
using Lindon.TowerUpper.GameController;
using Lindon.TowerUpper.GameController.Events;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float m_Speed = 1;
    [SerializeField,Min(1)] private int m_Damage = 1;
    private Rigidbody m_Rigidbody;
    private float m_floor;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnEnable()
    {
        transform.SetParent(GameManager.Instance.Tower.transform);
        m_floor = GameManager.Instance.Tower.Components.Floor.transform.position.y;

        transform.LookAt(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
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

        }
        else
        {

        }
    }

    private void GameFinished()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!GameRunnig.IsRunning) return;

        transform.position += m_Speed * Time.deltaTime * Vector3.down;

        if (transform.position.y <= m_floor)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Destroy(gameObject);
            enemy.Health.TakeDamage(m_Damage);
            enemy.Falling.FallDown(m_Rigidbody.velocity.magnitude * force);
        }
    }
}
