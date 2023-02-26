using Lindon.Framwork.Audio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lindon.Framwork.Audio.Component
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] List<AudioData> audioDatas;
        Dictionary<AudioDataType, List<AudioData>> datas;
        List<AudioData> hasUpdateDatas = new List<AudioData>();

        [Header("Systematically Sound")]
        [SerializeField, Tooltip("This audio source is used to play non-random sounds")]
        private AudioSource systematicallySource;
        private bool hasSystematicallyData { get { return datas[AudioDataType.Systematically].Count > 0; } }

        [Header("Randomly Sound")]
        [SerializeField, Tooltip("This Audio Source is used to randomly play sounds")]
        private AudioSource randomlyAudioSource;
        private bool hasRandomlyData { get { return datas[AudioDataType.Randomly].Count > 0; } }

        [Header("Timely Sound")]
        [SerializeField, Tooltip("This Audio Source is used to play sounds with pause and play time")]
        private AudioSource timelyAudioSource;
        private bool hasTimelyData { get { return datas[AudioDataType.Timely].Count > 0; } }

        [Header("Continued Sound")]
        [SerializeField, Tooltip("")]
        private AudioSource continuedAudioSource;
        private bool hasContinuedData { get { return datas[AudioDataType.Continued].Count > 0; } }

        [Header("Culling Randomly Sound")]
        [SerializeField, Tooltip(""),]
        private AudioSource cullingRandomlyAudioSource;
        private bool hasCullingRandomlyData { get { return datas[AudioDataType.CullingRandomly].Count > 0; } }

        #region Initialize

        private void Start()
        {
            CreateDatas();
            CheckDatas();

            if (systematicallySource)
                datas[AudioDataType.Systematically].ForEach(x => x.Initialize(systematicallySource));
            if (randomlyAudioSource)
                datas[AudioDataType.Randomly].ForEach(x => x.Initialize(randomlyAudioSource));
            if (timelyAudioSource)
                datas[AudioDataType.Timely].ForEach(x => x.Initialize(timelyAudioSource));
            if (continuedAudioSource)
                datas[AudioDataType.Continued].ForEach(x => x.Initialize(continuedAudioSource));
            if (cullingRandomlyAudioSource)
                datas[AudioDataType.CullingRandomly].ForEach(x => x.Initialize(cullingRandomlyAudioSource));

            hasUpdateDatas = new List<AudioData>();
            hasUpdateDatas.AddRange(datas.SelectMany(data => data.Value).Where(data => data.HasUpdate));
        }

        private void OnValidate()
        {
            if (audioDatas == null) return;

            CreateDatas();
            CheckDatas();
        }

        private void CreateDatas()
        {
            datas = new Dictionary<AudioDataType, List<AudioData>>();
            var values = Enum.GetValues(typeof(AudioDataType)).Cast<AudioDataType>();
            foreach (var val in values)
            {
                datas.Add(val, new List<AudioData>());
            }
        }

        private void CheckDatas()
        {
            foreach (var data in audioDatas)
            {
                if (data == null) continue;
                if (datas.ContainsKey(data.Type))
                {
                    datas[data.Type].Add(data);
                }
                else
                {
                    datas.Add(data.Type, new List<AudioData>() { data });
                }
            }
        }

        private void Reset()
        {
            CreateDatas();
        }

        #endregion

        #region Systematically Datas Functions

        /// <summary>
        /// play audio from systematically source
        /// </summary>
        /// <param name="tag">find this audio data tag, and play it</param>
        public void PlayAudio(string tag)
        {

            PlayAudio(systematicallySource, tag);
        }

        /// <summary>
        /// stop audio from systematically source
        /// </summary>
        public void StopAudio()
        {
            StopAudio(systematicallySource);
        }

        #endregion

        #region Other Datas Functions

        private void Update()
        {
            hasUpdateDatas.ForEach(x => x.Update());
            //datas.SelectMany(data => data.Value).Where(data => data.HasUpdate).ToList().ForEach(x => x.Update());
        }

        #endregion

        #region Global Functions

        #region Get Functions

        public AudioData GetAudioData(string tag)
        {
            if (audioDatas == null)
            {
                return null;
            }
            return audioDatas.FirstOrDefault(x => x.Tag == tag);
        }

        public AudioData GetAudioData(AudioDataType type, string tag)
        {
            if (audioDatas == null)
            {
                return null;
            }
            return GetAudioDatas(type).FirstOrDefault(x => x.Tag == tag);
        }

        public List<AudioData> GetAudioDatas(AudioDataType type)
        {
            if (audioDatas == null)
            {
                return null;
            }
            return audioDatas.FindAll(x => x.Type == type);
        }

        public List<string> GetAudioDatasName()
        {
            if (audioDatas == null)
            {
                return new List<string>();
            }
            return audioDatas.Where(x => !x.HasUpdate).Select(x => x.Tag).ToList();
        }

        #endregion

        /// <summary>
        /// find this audio data tag and cull <see cref="AudioData.Update"/> function when you want
        /// </summary>
        public void Update(string tag)
        {
            foreach (var val in datas.SelectMany(data => data.Value.Where(val => val.Tag == tag)))
            {
                val.Update();
            }
        }

        public void PlayAudio(AudioSource source, string tag)
        {
            if (Application.isBatchMode)
                return;
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var audioData = GetAudioData(tag);
            if (audioData == null) return;
            audioData.Play(source);
        }

        public void StopAudio(AudioSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.Stop();
            source.enabled = false;
        }

        public void StopAudio(string tag)
        {
            var audioData = GetAudioData(tag);
            if (audioData == null) return;
            audioData.Stop();
        }

        #endregion
    }
}