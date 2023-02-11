using UnityEngine;

namespace Lindon.TowerUpper.Profile
{
    public class ProfileController : MonoBehaviour
    {
        public static ProfileController Instance { get; private set; }

        public Profile Profile { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                LoadProfile(new Profile(SystemInfo.deviceUniqueIdentifier, 0, 1000));

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}