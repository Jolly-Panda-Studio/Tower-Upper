using Lindon.Framwork.Audio.Component;
using Lindon.TowerUpper.GameController.Events;
using UnityEngine;

namespace Lindon.TowerUpper.Enviroment
{
    [RequireComponent(typeof(AudioPlayer))]
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioPlayer m_player;

        [Header("Audio tags")]
        [SerializeField] private string m_HomeTag;
        [SerializeField] private string m_GameTag;

        private void Awake()
        {
            m_player = GetComponent<AudioPlayer>();
        }

        private void OnEnable()
        {
            ReturnHome.OnReturnHome += HomeMusic;
            GameStarter.OnStartGame += GameMusic;
        }

        private void OnDisable()
        {
            ReturnHome.OnReturnHome -= HomeMusic;
            GameStarter.OnStartGame -= GameMusic;
        }

        private void HomeMusic()
        {
            m_player.StopAudio();
            m_player.PlayAudio(m_HomeTag);
        }

        private void GameMusic()
        {
            m_player.StopAudio();
            m_player.PlayAudio(m_GameTag);
        }
    }
}