using Lindon.TowerUpper.Data;
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

                LoadProfile(CreateSampleProfile());

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
            var sampleProfile = new Profile(SystemInfo.deviceUniqueIdentifier, 0, 1000);
            sampleProfile.SetActiveItem(200, Data.ItemCategory.Weapon);
            sampleProfile.SetActiveItem(5001, Data.ItemCategory.Skin);
            return sampleProfile;
        }

        public void LoadProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}