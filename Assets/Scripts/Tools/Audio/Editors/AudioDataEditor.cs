using Lindon.Framwork.Audio.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lindon.Framwork.Audio.Editors
{
    [CustomEditor(typeof(AudioData))]
    public class AudioDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}