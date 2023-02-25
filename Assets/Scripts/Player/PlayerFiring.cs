using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    private Weapon m_ActiveWeapon;
    private bool m_GunSelected = false;

    private void Update()
    {
        if (!m_GunSelected) return;

        if (Input.GetMouseButton(0))
        {
            m_ActiveWeapon.Fire();
        }
    }

    public void ActiveGun(Weapon weapon)
    {
        m_GunSelected = true;
        m_ActiveWeapon = weapon;
    }
}
