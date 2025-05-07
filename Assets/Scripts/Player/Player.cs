using JollyPanda.LastFlag.Handlers;
using UnityEngine;

namespace JollyPanda.LastFlag.PlayerModule
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private GunSelector gunSelector;
        [SerializeField] private PlayerAnimationController animationController;

        private void OnEnable()
        {
            gunSelector.OnGunChange += GunChanged;
            Informant.OnLose += Losing;
        }

        private void OnDisable()
        {
            gunSelector.OnGunChange -= GunChanged;
            Informant.OnLose -= Losing;
        }

        private void GunChanged(Gun activeGun)
        {
        }

        private void Losing()
        {
            gunSelector.DisableGun();
            animationController.PlayDefeat();
        }
    }
}