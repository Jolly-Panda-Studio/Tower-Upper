using UnityEngine;

namespace JollyPanda.LastFlag.Database
{
    public static class SaveSystem
    {
        private const string SaveKey = "PlayerData";

        public static void Save(PlayerSaveData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public static PlayerSaveData Load()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                string json = PlayerPrefs.GetString(SaveKey);
                return JsonUtility.FromJson<PlayerSaveData>(json);
            }
            return PlayerSaveData.Default;
        }
    }

}