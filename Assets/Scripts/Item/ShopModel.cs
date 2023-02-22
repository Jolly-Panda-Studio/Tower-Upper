using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class ShopModel : MonoBehaviour
    {
        [SerializeField] private ItemData m_Data;

        [SerializeField] private GameModel m_GameModel;

        public int Id => m_Data.Id;

        public ItemData Data => m_Data;

        public GameModel GetGameModel()
        {
            if (m_GameModel == null)
            {
                Debug.LogWarning("This character type does not exist in this model");
                return null;
            }
            return m_GameModel;
        }

        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}