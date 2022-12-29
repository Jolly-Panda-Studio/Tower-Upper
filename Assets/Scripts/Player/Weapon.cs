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

    //bools
    bool readyToShoot;
    //private bool allowInvoke = true;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Fire();
    //    }
    //}

    public void Fire()
    {
        

        var muzzle = muzzles[(muzzleIndex++) % muzzles.Count];
        attackPoint = muzzle;

        var currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        currentBullet.GetComponent<Rigidbody>().AddForce(Vector3.down * shootForce, ForceMode.Impulse);

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

    }

    //private void Shooting(bool shooting)
    //{

    //    //Shooting
    //    if (readyToShoot && shooting)
    //    {
    //        //Set bullets shot to 0

    //        Shoot();
    //    }
    //}

    //private void Shoot()
    //{
    //    readyToShoot = false;

    //    var muzzle = muzzles[(muzzleIndex++) % muzzles.Count];
    //    attackPoint = muzzle;

    //    //Find the exact hit position using a raycast
    //    Ray ray = new Ray(muzzle.position, muzzle.forward);
    //    //Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
    //    RaycastHit hit;

    //    //check if ray hits something
    //    Vector3 targetPoint;
    //    if (Physics.Raycast(ray, out hit))
    //        targetPoint = hit.point;
    //    else
    //        targetPoint = ray.GetPoint(75); //Just a point far away from the player

    //    //Calculate direction from attackPoint to targetPoint
    //    Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

    //    //Calculate new direction with spread
    //    Vector3 directionWithSpread = directionWithoutSpread; //Just add spread to last direction


    //    //Instantiate bullet/projectile
    //    var currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation); //store instantiated bullet in currentBullet
    //    //Rotate bullet to shoot direction
    //    currentBullet.transform.forward = directionWithSpread.normalized;

    //    //Add forces to bullet
    //    currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

    //    //Instantiate muzzle flash, if you have one
    //    if (muzzleFlash != null)
    //        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);


    //    //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
    //    if (allowInvoke)
    //    {
    //        Invoke("ResetShot", timeBetweenShooting);
    //        allowInvoke = false;
    //    }

    //    //if more than one bulletsPerTap make sure to repeat shoot function
    //        Invoke("Shoot", timeBetweenShots);
    //}
    //private void ResetShot()
    //{
    //    //Allow shooting and invoking again
    //    readyToShoot = true;
    //    allowInvoke = true;
    //}
}