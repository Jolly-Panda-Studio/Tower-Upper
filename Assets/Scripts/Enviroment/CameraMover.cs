using Cinemachine;
using DG.Tweening;
using Lindon.TowerUpper.GameController.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_Camera;
    [SerializeField] private Transform m_InHomePosition;
    [SerializeField] private Transform m_InGamePosition;
    [SerializeField] private float m_Speed = 1;

    private void Start()
    {
        m_Camera.transform.position = m_InHomePosition.position;
    }

    private void OnEnable()
    {
        GameStarter.OnStartGame += GoGamePosition;
        ReturnHome.OnReturnHome += GoHomePosition;
    }

    private void OnDisable()
    {
        GameStarter.OnStartGame -= GoGamePosition;
        ReturnHome.OnReturnHome -= GoHomePosition;
    }

    private void GoGamePosition()
    {
        SwitchPosition(m_InGamePosition.position);
    }

    private void GoHomePosition()
    {
        SwitchPosition(m_InHomePosition.position);
    }

    private void SwitchPosition(Vector3 endValue)
    {
        m_Camera.transform.DOMove(endValue, transform.GetDuration(endValue, m_Speed));
    }
}
