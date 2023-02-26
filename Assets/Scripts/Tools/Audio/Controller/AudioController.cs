using UnityEngine;

namespace Lindon.Framwork.Audio.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance { get; private set; }

        private AudioDatabase m_Database;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                LoadComponents();
                LoadDatabase();

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadComponents()
        {
            m_Database = new AudioDatabase();
        }

        private void LoadDatabase()
        {
            m_Database.Load();
        }

        private void SaveDatabase()
        {
            m_Database.Save();
        }

        private void OnApplicationQuit()
        {
            SaveDatabase();
        }
    }
}