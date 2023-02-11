using Lindon.TowerUpper.Initilizer;
using UnityEngine;

namespace Lindon.TowerUpper.Profile
{
    public class ProfileController : MonoBehaviour, IInitilizer
    {
        public static ProfileController Instance { get; private set; }

        public Profile Profile { get; private set; }

        public void Init()
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