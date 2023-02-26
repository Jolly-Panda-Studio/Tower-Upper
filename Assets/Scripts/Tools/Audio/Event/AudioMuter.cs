using Lindon.Framwork.Audio.Data;
using System;

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
        public static event Action<AudioSourceType,bool> OnMute;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isMute"></param>
        public static void SetMute(AudioSourceType type, bool isMute)
        {
            OnMute?.Invoke(type, isMute);
        }
    }
}
