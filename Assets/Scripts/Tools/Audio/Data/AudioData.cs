using System.Collections.Generic;
using UnityEngine;

namespace Lindon.Framwork.Audio.Data
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Lindon/Framwork/Audio/AudioData")]
    public class AudioData : ScriptableObject
    {
        [SerializeField] private AudioDataType type;
        [SerializeField] private string tag;
        [SerializeField] private List<AudioClip> audioClips;

        [Header("Times")]
        [SerializeField, Tooltip("How many seconds is it between each time this sound is played?")]
        private Vector2 pausedTime = new Vector2(50, 50);
        [SerializeField, Tooltip("How many seconds does this sound play?")]
        private Vector2 playedTime = new Vector2(50, 50);

        private AudioSource source;
        private float currentPlayTime;
        private float currentPauseTime;

        private bool ShowPlayTime => type == AudioDataType.Timely;
        private bool ShowPauseTime => type == AudioDataType.Randomly || type == AudioDataType.Timely || type == AudioDataType.CullingRandomly;

        public string Tag => tag;
        public AudioDataType Type => type;
        /// <summary>
        /// include <see cref="AudioDataType.Randomly"/>, <see cref="AudioDataType.Continued"/> and <see cref="AudioDataType.Timely"/>
        /// </summary>
        public bool HasUpdate => type == AudioDataType.Randomly || type == AudioDataType.Continued || type == AudioDataType.Timely;

        public void Initialize(AudioSource source)
        {
            this.source = source;

            switch (type)
            {
                case AudioDataType.Systematically:
                    break;
                case AudioDataType.Randomly:
                    this.source.loop = false;
                    CalulatePauseTime();
                    currentPlayTime = 0;
                    break;
                case AudioDataType.Timely:
                    this.source.loop = true;
                    CalculatePlayTime();
                    currentPauseTime = 0;
                    break;
                case AudioDataType.Continued:
                    this.source.loop = false;
                    this.source.clip = GetAudioClip();
                    currentPlayTime = source.clip.length;
                    this.source.Play();
                    break;
                case AudioDataType.CullingRandomly:
                    this.source.loop = false;
                    this.source.clip = GetAudioClip();
                    currentPlayTime = source.clip.length;
                    this.source.Play();
                    break;
            }
        }

        public void Play(AudioSource source)
        {
            if (HasUpdate) return;

            source.enabled = true;
            source.clip = GetAudioClip();
            source.Play();
        }

        public void Play()
        {
            Play(source);
        }

        public void Stop()
        {
            if (HasUpdate) return;

            source.Stop();
            source.clip = null;
        }

        public void Update()
        {
            if (!HasUpdate || !source) return;

            switch (type)
            {
                case AudioDataType.Systematically:
                    return;
                case AudioDataType.Timely:
                    PlayAudio();
                    PauseAudio();
                    break;
                case AudioDataType.Randomly:
                    PauseAudio();
                    break;
                case AudioDataType.Continued:
                    PlayAudio();
                    break;
                case AudioDataType.CullingRandomly:
                    PlayAudio();
                    PauseAudio();
                    break;
            }
        }

        private void PlayAudio()
        {
            if (currentPlayTime > 0)
            {
                currentPlayTime -= Time.deltaTime;

                if (currentPlayTime <= 0)
                {
                    if (type == AudioDataType.Continued)
                    {
                        source.clip = GetAudioClip();
                        currentPlayTime = source.clip.length;
                        source.Play();
                    }
                    else
                    {
                        source.Stop();
                        source.clip = null;
                        CalulatePauseTime();
                    }
                }
            }
        }

        private void PauseAudio()
        {
            if (currentPauseTime > 0)
            {
                currentPauseTime -= Time.deltaTime;

                if (currentPauseTime <= 0)
                {
                    if (!source) return;
                    source.enabled = true;
                    source.clip = GetAudioClip();
                    source.Play();

                    if (type == AudioDataType.Randomly)
                    {
                        CalulatePauseTime();
                    }
                    else
                    {
                        CalculatePlayTime();
                    }
                }
            }
        }

        private void CalculatePlayTime()
        {
            currentPlayTime = type == AudioDataType.CullingRandomly ? source.clip.length : Random.Range(playedTime.x, playedTime.y);
        }

        private void CalulatePauseTime()
        {
            currentPauseTime = Random.Range(pausedTime.x, pausedTime.y);
        }

        private AudioClip GetAudioClip()
        {
            if (audioClips.Count == 0) return null;

            return audioClips.RandomItem();
        }

    }

}