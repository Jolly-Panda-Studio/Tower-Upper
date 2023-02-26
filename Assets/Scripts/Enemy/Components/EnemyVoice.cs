using Lindon.Framwork.Audio.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Lindon.TowerUpper.EnemyUtility.Component
{

    [RequireComponent(typeof(AudioPlayer))]
    public class EnemyVoice : BaseComponent
    {
        [SerializeField] private AudioPlayer m_player;

        [Header("Audio tags")]
        [SerializeField] private string m_FallingTag;
        [SerializeField] private string m_ClimbingTag;

        protected override void OnCreate()
        {
            m_player = GetComponent<AudioPlayer>();
        }

        public override void RegisterEvents()
        {
            m_Enemy.OnStartClimbing += PlayClimbingSound;
            m_Enemy.OnFalling += PlayFallSound;
        }

        public override void UnregisterEvents()
        {
            m_Enemy.OnStartClimbing -= PlayClimbingSound;
            m_Enemy.OnFalling -= PlayFallSound;
        }

        private void PlayClimbingSound(Enemy enemy)
        {
            m_player.StopAudio();
            m_player.PlayAudio(m_ClimbingTag);
        }

        private void PlayFallSound(Enemy enemy)
        {
            m_player.StopAudio();
            m_player.PlayAudio(m_FallingTag);
        }
    }
}