using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Ammo bullet;

    [Header("Bullet force")]
    [SerializeField] private float shootForce;

    [Header("Muzzles")]
    [SerializeField] private List<Transform> muzzles;
    private Transform attackPoint;
    int muzzleIndex = 0;

    [Header("Graphics")]
    [SerializeField] private GameObject muzzleFlash;

    [Header("Aim")]
    [SerializeField] private Transform m_Aim;

    public Transform GetAim() => m_Aim;

    public void Fire()
    {
        var muzzle = muzzles[(muzzleIndex++) % muzzles.Count];
        attackPoint = muzzle;

        var currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        currentBullet.GetComponent<Rigidbody>().AddForce(Vector3.down * shootForce, ForceMode.Impulse);

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

    }
}