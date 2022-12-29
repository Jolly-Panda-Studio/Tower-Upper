using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    private int m_Index;

    bool readyToShoot;

    [SerializeField] private float timeBetweenShooting = 1;

    private void Start()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        if (readyToShoot && Input.GetMouseButton(0))
        {
            readyToShoot = false;
            var index = m_Index++ % weapons.Count;
            weapons[index].Fire();

            Invoke(nameof(ResetShot), timeBetweenShooting);

        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
    }
}
