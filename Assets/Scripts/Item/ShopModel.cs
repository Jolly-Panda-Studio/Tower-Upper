using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class ShopModel : MonoBehaviour
    {
        [SerializeField] private ItemData m_Data;

        public int Id => m_Data.Id;

        public ItemData Data => m_Data;

        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}