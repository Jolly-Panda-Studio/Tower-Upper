using Lindon.Framwork.Audio.Data;
using Lindon.Framwork.Audio.Event;
using Unity.Collections;
using UnityEngine;

namespace Lindon.Framwork.Audio.Component
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceDetector : MonoBehaviour
    {
        [SerializeField] private AudioSourceType m_Type;
        [SerializeField, ReadOnly] private AudioSource audioSource;

        private void Reset()
        {
            CheckAudioSource();
        }

        private void Start()
        {
            CheckAudioSource();
        }

        private void OnEnable()
        {
            AudioVolumeChanger.OnChange += OnChangeVolume;
            AudioMuter.OnMute += OnMute;
        }

        private void OnDisable()
        {
            AudioVolumeChanger.OnChange += OnChangeVolume;
            AudioMuter.OnMute += OnMute;
        }

        private void OnMute(AudioSourceType type, bool isMute)
        {
            if (m_Type == type)
            {
                audioSource.mute = isMute;
            }
        }

        private void OnChangeVolume(AudioSourceType type, float volumeValue)
        {
            if (m_Type == type)
            {
                audioSource.volume = volumeValue;
            }
        }

        [ContextMenu("Check Audio Source")]
        private void CheckAudioSource()
        {
            audioSource ??= GetComponent<AudioSource>();
        }
    }
}