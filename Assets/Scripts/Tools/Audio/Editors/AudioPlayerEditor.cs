using Lindon.Framwork.Audio.Component;
using UnityEditor;

namespace Lindon.Framwork.Audio.Editors
{
    [CustomEditor(typeof(AudioPlayer))]
    public class AudioPlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}