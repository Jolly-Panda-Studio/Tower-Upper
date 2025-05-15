using JollyPanda.LastFlag.Handlers;
using System;
using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{
    public class GunSelector : MonoBehaviour
    {
        [SerializeField] private Gun[] guns;

        private Gun activeGun;

        public event Action<Gun> OnGunChange;

        private void OnEnable()
        {
            Informant.OnStart += ActiveGun;
        }

        private void OnDisable()
        {
            Informant.OnStart -= ActiveGun;
        }

        private void ActiveGun()
        {
            activeGun.SetActive(true);
        }

        private void Start()
        {
            if (guns.Length > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, guns.Length);
                SelectGun(guns[randomIndex].Id);
            }
        }

        public void SelectGun(int gunID)
        {
            foreach (var gun in guns)
            {
                if (gun.Id == gunID)
                {
                    gun.gameObject.SetActive(true);

                    activeGun = gun;
                    //activeGun.SetActive(true);

                    OnGunChange?.Invoke(activeGun);
                }
                else
                {
                    gun.gameObject.SetActive(false);
                }
            }
        }

        internal void DisableGun()
        {
            activeGun.SetActive(false);
        }
    }
}