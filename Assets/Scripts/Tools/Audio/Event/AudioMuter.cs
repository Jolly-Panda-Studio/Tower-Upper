using Lindon.Framwork.Audio.Data;
using Lindon.TowerUpper.GameController.Events;
using Lindon.TowerUpper.Initilizer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.Framwork.Audio.Event
{
    /// <summary>
    /// 
    /// </summary>
    public static class AudioMuter
    {
        /// <summary>
        /// 
        /// </summary>
        public static event Action<AudioSourceType, bool> OnMute;

        private static Dictionary<AudioSourceType, bool> m_MuteState;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            m_MuteState = new Dictionary<AudioSourceType, bool>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isMute"></param>
        public static void SetMute(AudioSourceType type, bool isMute)
        {
            if (m_MuteState.ContainsKey(type))
            {
                m_MuteState[type] = isMute;
            }
            else
            {
                m_MuteState.Add(type, isMute);
            }
            OnMute?.Invoke(type, isMute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsMute(AudioSourceType type)
        {
            if (m_MuteState.ContainsKey(type))
            {
                return m_MuteState[type];
            }
            else
            {
                m_MuteState.Add(type, false);
                return false;
            }
        }
    }
}