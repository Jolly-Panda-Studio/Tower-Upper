using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class ItemModel : MonoBehaviour
    {
        [SerializeField] private ItemData m_Data;

        [SerializeField] private SubModel m_SubModel;

        public ItemData Data => m_Data;

        public int Id => m_Data.Id;

        public SubModel GetSubModel() => m_SubModel;

        public bool Equals(int id)
        {
            return m_Data.Id == id;
        }
    }
}