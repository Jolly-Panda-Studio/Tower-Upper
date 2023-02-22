using Lindon.TowerUpper.GameController.Events;
using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AimIK))]
public class PlayerAimingAssistant : MonoBehaviour
{
    [SerializeField] private AimIK m_AimIK;

    private Transform m_aimTransform;
    private Transform m_aimTarget;

    private void Awake()
    {
        m_AimIK = GetComponent<AimIK>();
    }

    private void OnEnable()
    {
        GameStarter.OnStartGame += StartGame;
        GameFinisher.OnFinishGame += FinishGame;
    }

    private void OnDisable()
    {
        GameStarter.OnStartGame -= StartGame;
        GameFinisher.OnFinishGame -= FinishGame;
    }

    private void FinishGame()
    {
        m_AimIK.solver.target = null;
        m_AimIK.solver.transform = null;
    }

    private void StartGame()
    {
        m_AimIK.solver.target = m_aimTarget;
        m_AimIK.solver.transform = m_aimTransform;
    }

    public void SetAimTarget(Transform aimTarget)
    {
        m_aimTarget = aimTarget;
    }

    public void SetAimTransform(Transform aimTransform)
    {
        m_aimTransform = aimTransform;
    }

    public void SetActive(bool value)
    {
        if (value)
        {
            m_AimIK.solver.IKPositionWeight = 1;
        }
        else
        {
            m_AimIK.solver.IKPositionWeight = 0;
        }
    }
}
