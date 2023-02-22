using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.Initilizer;
using System;
using UnityEngine;

namespace Lindon.TowerUpper.Profile
{
    public class ProfileController : MonoBehaviour, IInitilizer
    {
        public static ProfileController Instance { get; private set; }

        public Profile Profile { get; private set; }

        public event Action<Profile> OnLoadProfile;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;

                LoadProfile(CreateSampleProfile());

                OnLoadProfile?.Invoke(Profile);

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            Profile.OnAddItem += Profile_OnAddItem;
        }

        private void Profile_OnAddItem(int itemId)
        {
            var category = GameData.Instance.GetItemByCategory(itemId);
            Profile.SetActiveItem(itemId, category);
        }

        private void OnDisable()
        {
            Profile.OnAddItem -= Profile_OnAddItem;
        }

        private Profile CreateSampleProfile()
        {
            Debug.Log("Sample Profile Created!");
            var sampleProfile = new Profile(SystemInfo.deviceUniqueIdentifier, 15, 1000);
            sampleProfile.SetActiveItem(8001, Data.ItemCategory.Weapon);
            sampleProfile.SetActiveItem(5001, Data.ItemCategory.Skin);
            return sampleProfile;
        }

        public void LoadProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}