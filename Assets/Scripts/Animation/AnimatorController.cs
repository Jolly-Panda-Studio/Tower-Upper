using Lindon.TowerUpper.GameController.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.AnimatorController
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] protected Animator m_animator;
        protected AnimatorOverrideController animatorOverrideController;

        [SerializeField] private Armory m_Armory;

        [Header("Parameters")]
        [SerializeField] private string m_IdleParameter;

        [Header("Idle")]
        [SerializeField] private string m_IdleName;
        [SerializeField] private CharacterClip[] m_Idles;

        // Start is called before the first frame update
        void Start()
        {
            animatorOverrideController = new AnimatorOverrideController(m_animator.runtimeAnimatorController);
            m_animator.runtimeAnimatorController = animatorOverrideController;

            PlayIdle();
        }

        private void OnEnable()
        {
            GameStarter.OnStartGame += StartGame;
            GameFinisher.OnFinishGame += FinishGame;
            GameRunnig.OnChange += SetAnimatorActive;
        }

        private void OnDisable()
        {
            GameStarter.OnStartGame -= StartGame;
            GameFinisher.OnFinishGame -= FinishGame;
            GameRunnig.OnChange -= SetAnimatorActive;
        }

        protected virtual void FinishGame()
        {
            PlayIdle();
        }

        protected virtual void StartGame()
        {

        }

        public void PlayIdle()
        {
            var idle = m_Idles.RandomItem();
            animatorOverrideController[m_IdleName] = idle.Clip;

            m_Armory.SetActive(idle.ShowWeapon);
        }

        public void SetAnimatorActive(bool value)
        {
            if (value)
            {
                m_animator.speed = 1;
            }
            else
            {
                m_animator.speed = 0;
            }
        }

        [System.Serializable]
        private class CharacterClip
        {
            public AnimationClip Clip;
            public bool ShowWeapon;
        }
    }
}