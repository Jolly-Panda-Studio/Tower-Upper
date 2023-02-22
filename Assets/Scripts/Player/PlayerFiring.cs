using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private float m_timeBetweenShooting = 1;
    private Weapon m_ActiveWeapon;
    private bool m_readyToShoot;
    private bool m_GunSelected = false;

    private void Start()
    {
        m_readyToShoot = true;
    }

    private void Update()
    {
        if (!m_GunSelected) return;

        if (m_readyToShoot && Input.GetMouseButton(0))
        {
            m_readyToShoot = false;
            m_ActiveWeapon.Fire();

            Invoke(nameof(ResetShot), m_timeBetweenShooting);
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        m_readyToShoot = true;
    }

    public void ActiveGun(Weapon weapon)
    {
        m_GunSelected = true;
        m_ActiveWeapon = weapon;
    }
}
