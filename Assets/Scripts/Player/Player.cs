using JollyPanda.LastFlag.Handlers;
using System;
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
            Informant.OnChangeUIPage += ChoicePlayerAnimation;
        }

        private void OnDisable()
        {
            gunSelector.OnGunChange -= GunChanged;
            Informant.OnLose -= Losing;
            Informant.OnChangeUIPage -= ChoicePlayerAnimation;
        }

        private void GunChanged(Gun activeGun)
        {
        }

        private void Losing()
        {
            gunSelector.DisableGun();
            animationController.PlayDefeat();
        }

        private void ChoicePlayerAnimation(PageType type)
        {
            switch (type)
            {
                case PageType.HUD:
                    animationController.PlayShooting(true);
                    break;
                case PageType.Home:
                    animationController.PlayShooting(false);
                    animationController.PlayIdle();
                    gunSelector.DisableGun();
                    break;
            }
        }
    }
}