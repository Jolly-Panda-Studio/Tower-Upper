using System;
using UnityEngine;

namespace JollyPanda.LastFlag.Database
{
    public static class SaveSystem
    {
        private const string SaveKey = "PlayerData";

        public static event Action<PlayerSaveData> OnApplyChange;

        public static void Save(PlayerSaveData data)
        {
            ES3.Save(SaveKey, data);
            OnApplyChange?.Invoke(data);
        }

        public static PlayerSaveData Load()
        {
            if (ES3.KeyExists(SaveKey))
            {
                return ES3.Load<PlayerSaveData>(SaveKey);
            }

            return PlayerSaveData.Default;
        }
    }
}