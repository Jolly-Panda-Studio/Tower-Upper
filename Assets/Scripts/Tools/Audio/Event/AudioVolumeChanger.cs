using Lindon.Framwork.Audio.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.Framwork.Audio.Event
{
    /// <summary>
    /// 
    /// </summary>
    public static class AudioVolumeChanger
    {
        public const float MinVolume = 0;
        public const float MaxVolume = 1;

        /// <summary>
        /// 
        /// </summary>
        public static event Action<AudioSourceType,float> OnChange;

        private static Dictionary<AudioSourceType, float> m_VolumeState;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            m_VolumeState = new Dictionary<AudioSourceType, float>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public static void ChangeVolume(AudioSourceType type, float value)
        {
            var volume = Mathf.Clamp(value, MinVolume, MaxVolume);
            if (m_VolumeState.ContainsKey(type))
            {
                m_VolumeState[type] = volume;
            }
            else
            {
                m_VolumeState.Add(type, volume);
            }
            OnChange?.Invoke(type, volume);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static float GetVolume(AudioSourceType type)
        {
            if (m_VolumeState.ContainsKey(type))
            {
                return m_VolumeState[type];
            }
            else
            {
                m_VolumeState.Add(type, MaxVolume);
                return MinVolume;
            }
        }
    }
}
