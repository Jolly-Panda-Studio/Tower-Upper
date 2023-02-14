using Lindon.TowerUpper.Data;
using UnityEngine;

public class Armory : MonoBehaviour
{
    [SerializeField] private ItemModel[] m_WeaponModels;

    public Weapon ActiveWeapon(int weaponId)
    {
        Weapon weapon = null;
        foreach (var weaponModel in m_WeaponModels)
        {
            weaponModel.gameObject.SetActive(false);

            if (weaponModel.Equals(weaponId) && weapon == null)
            {
                weaponModel.gameObject.SetActive(true);
                weapon = weaponModel.GetComponent<Weapon>();
            }
        }
        return weapon;
    }
}
