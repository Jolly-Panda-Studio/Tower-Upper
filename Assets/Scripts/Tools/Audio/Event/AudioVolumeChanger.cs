using Lindon.Framwork.Audio.Data;
using System;

namespace Lindon.Framwork.Audio.Event
{
    /// <summary>
    /// 
    /// </summary>
    public static class AudioVolumeChanger
    {
        /// <summary>
        /// 
        /// </summary>
        public static event Action<AudioSourceType,float> OnChange;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public static void ChangeVolume(AudioSourceType type, float value)
        {
            OnChange?.Invoke(type, value);
        }
    }
}
