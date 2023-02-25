using Lindon.TowerUpper.GameController.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float m_timeBetweenShooting = 1;
    private bool m_readyToShoot;

    [Header("Bullet")]
    [SerializeField] private Ammo bullet;

    [Header("Muzzles")]
    [SerializeField] private List<Transform> muzzles;
    private Transform attackPoint;
    int muzzleIndex = 0;

    [Header("Graphics")]
    [SerializeField] private GameObject muzzleFlash;

    [Header("Aim")]
    [SerializeField] private Transform m_Aim;

    public Transform GetAim() => m_Aim;

    private void Start()
    {
        m_readyToShoot = true;
    }

    public void SettBullet(Ammo ammo)
    {
        bullet = ammo;
    }

    public void Fire()
    {
        if (!GameRunnig.IsRunning) return;
        if (!m_readyToShoot) return;
        m_readyToShoot = false;
        var muzzle = muzzles[(muzzleIndex++) % muzzles.Count];
        attackPoint = muzzle;

        Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        Invoke(nameof(ResetShot), m_timeBetweenShooting);
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        m_readyToShoot = true;
    }
}