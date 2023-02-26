using Lindon.Framwork.Database;
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

        private const string ProfileTag = "PROFILE";

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;

                LoadProfile();

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            OnLoadProfile?.Invoke(Profile);
        }


        private void OnEnable()
        {
            Profile.OnAddItem += Profile_OnAddItem;
        }

        private void OnDisable()
        {
            Profile.OnAddItem -= Profile_OnAddItem;
        }

        private void Profile_OnAddItem(int itemId)
        {
            var category = GameData.Instance.GetItemByCategory(itemId);
            Profile.SetActiveItem(itemId, category);
        }

        public void LoadProfile()
        {
            if (DatabaseHandler.HasKey(ProfileTag))
            {
                Profile = new Profile();
                var jsonString = DatabaseHandler.GetString(ProfileTag);
                JSONObject json = new JSONObject(jsonString);
                Profile.Load(json);
            }
            else
            {
                Profile = CreateSampleProfile();
            }

            OnLoadProfile?.Invoke(Profile);
        }

        private Profile CreateSampleProfile()
        {
            Debug.Log("Sample Profile Created!");
            var sampleProfile = new Profile(0, 1000);
            sampleProfile.SetActiveItem(11, Data.ItemCategory.Weapon);
            sampleProfile.SetActiveItem(5001, Data.ItemCategory.Skin);

            sampleProfile.BuyItem(11, 0);
            sampleProfile.BuyItem(5001, 0);
            return sampleProfile;
        }

        private void OnApplicationQuit()
        {
            var json = Profile.Save().ToString();
            DatabaseHandler.SetString(ProfileTag, json);
        }
    }
}