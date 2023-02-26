using Lindon.Framwork.Audio.Data;
using Lindon.Framwork.Audio.Event;
using Lindon.Framwork.Database;
using System;

namespace Lindon.Framwork.Audio.Controller
{
    public class AudioDatabase
    {
        public static event Action<AudioController> OnLoad;

        private string GetTag(AudioSourceType value, string extension)
        {
            var tag = $"{value.ToString().ToUpper()}_{extension}";
            return tag;
        }

        public void Load()
        {
            foreach (var value in Enum.GetValues(typeof(AudioSourceType)))
            {
                var key = (AudioSourceType)value;

                string muteTag = GetTag((AudioSourceType)value, "MUTE");
                var isMute = DatabaseHandler.GetBool(muteTag);
                AudioMuter.SetMute(key, isMute);

                string volumeTag = GetTag((AudioSourceType)value, "VOLUME");
                var volume = DatabaseHandler.GetFloat(volumeTag);
                AudioVolumeChanger.ChangeVolume(key, volume);
            }

            OnLoad?.Invoke(AudioController.Instance);
        }

        public void Save()
        {
            foreach (var value in Enum.GetValues(typeof(AudioSourceType)))
            {
                var key = (AudioSourceType)value;

                string muteTag = GetTag((AudioSourceType)value, "MUTE");
                var isMute = AudioMuter.IsMute(key);
                DatabaseHandler.SetBool(muteTag, isMute);

                string volumeTag = GetTag((AudioSourceType)value, "VOLUME");
                var volume = AudioVolumeChanger.GetVolume(key);
                DatabaseHandler.SetFloat(volumeTag, volume);
            }
        }
    }
}