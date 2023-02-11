using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class ItemModel : MonoBehaviour
    {
        [SerializeField] private ItemData m_Data;

        public ItemData Data => m_Data;

        public int Id => m_Data.Id;


        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}