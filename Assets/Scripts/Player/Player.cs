using Lindon.TowerUpper.GameController.Events;
using RootMotion.FinalIK;
using System;
using UnityEngine;

[RequireComponent(typeof(AimIK))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AimIK m_Aim;
    [SerializeField] private Armory m_Armory;
    [SerializeField] private PlayerFiring m_Firing;
    [SerializeField] private PlayerAnimator m_Animator;
    [SerializeField] private PlayerAimingAssistant m_AimingAssistant;

    private void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        m_Aim = GetComponent<AimIK>();
        m_Armory ??= GetComponentInChildren<Armory>();
        m_Firing ??= GetComponentInChildren<PlayerFiring>();
        m_Animator ??= GetComponentInChildren<PlayerAnimator>();
        m_AimingAssistant ??= GetComponentInChildren<PlayerAimingAssistant>();
    }

    public Weapon ActiveGun(int weaponId)
    {
        var weapon = m_Armory.ActiveWeapon(weaponId);
        m_Firing.ActiveGun(weapon);
        return weapon;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The target Transform. Solver IKPosition will be automatically set to the position of the target.</param>
    /// <returns></returns>
    public Player SetAimTarget(Transform target)
    {
        m_AimingAssistant.SetAimTarget(target);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform">The transform that we want to aim at IKPosition.</param>
    /// <returns></returns>
    public Player SetAimTransform(Transform transform)
    {
        m_AimingAssistant.SetAimTransform(transform);
        return this;
    }
}
