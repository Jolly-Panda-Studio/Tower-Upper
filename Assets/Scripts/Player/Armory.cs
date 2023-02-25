using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Profile;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    [SerializeField] private List<Ammo> m_ammos;
    [SerializeField] private List<WeaponModel> m_WeaponModels;
    private int m_lastId = -1;

    private void OnEnable()
    {
        GameStarter.OnStartGame += StartGame;
    }

    private void OnDisable()
    {
        GameStarter.OnStartGame -= StartGame;
    }

    private void StartGame()
    {
        if (m_lastId == -1) return;
        var activatedWeapon = ActiveWeapon(m_lastId);
        SetAmmo(activatedWeapon);
    }

    public bool AddWeapon(WeaponModel newModel)
    {
        foreach(var model in m_WeaponModels)
        {
            if (model.Equals(newModel.Id))
            {
                return false;
            }
        }
        m_WeaponModels.Add(newModel);
        return true;
    }

    private void SetAmmo(Weapon weapon)
    {
        var bullet = m_ammos.RandomItem();
        weapon.SettBullet(bullet);
    }

    public Weapon ActiveWeapon(int weaponId)
    {
        m_lastId = weaponId;

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

    public void SetActive(bool value)
    {
        if (m_lastId == -1)
        {
            m_lastId = ProfileController.Instance.Profile.GetActiveItem(ItemCategory.Weapon);
        }

        foreach (var weaponModel in m_WeaponModels)
        {
            weaponModel.gameObject.SetActive(false);
            if (value && weaponModel.Equals(m_lastId))
            {
                weaponModel.gameObject.SetActive(true);
            }
        }
    }
}
