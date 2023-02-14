using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

[RequireComponent(typeof(AimIK))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform m_WeaponParent;
    //[SerializeField] private List<Weapon> weapons;
    [SerializeField] private Weapon m_ActiveWeapon;
    private int m_Index;
    [SerializeField] private float timeBetweenShooting = 1;
    bool readyToShoot;

    [Header("Components")]
    [SerializeField] private AimIK m_Aim;
    [SerializeField] private Armory m_Armory;

    private void Awake()
    {
        LoadComponents();
    }

    private void Start()
    {
        readyToShoot = true;

        Debug.Log(m_Aim.solver.transform, m_Aim.solver.transform);
    }

    private void LoadComponents()
    {
        m_Aim = GetComponent<AimIK>();
        m_Armory ??= GetComponentInChildren<Armory>();
    }

    private void Update()
    {
        if (readyToShoot && Input.GetMouseButton(0))
        {
            readyToShoot = false;
            //var index = m_Index++ % weapons.Count;
            m_ActiveWeapon.Fire();

            Invoke(nameof(ResetShot), timeBetweenShooting);

        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
    }

    public Weapon ActiveGun(int weaponId)
    {
        m_ActiveWeapon = m_Armory.ActiveWeapon(weaponId);
        return m_ActiveWeapon;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">The target Transform. Solver IKPosition will be automatically set to the position of the target.</param>
    /// <returns></returns>
    public Player SetAimTarget(Transform target)
    {
        m_Aim.solver.target = target;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform">The transform that we want to aim at IKPosition.</param>
    /// <returns></returns>
    public Player SetAimTransform(Transform transform)
    {
        m_Aim.solver.transform = transform;
        return this;
    }
}
