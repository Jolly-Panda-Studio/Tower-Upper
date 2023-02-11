using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }

        [SerializeField] private List<ItemData> m_Items;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public List<ItemData> GetItems() => m_Items;

        public ItemCategory GetItemCategory(int itemId)
        {
            foreach (var item in m_Items)
            {
                if (item.Equals(itemId))
                {
                    return item.Category;
                }
            }
            Debug.LogError($"Item with {itemId} was not found");
            return ItemCategory.None;
        }

        public ItemData GetItem(int itemId)
        {
            foreach (var item in m_Items)
            {
                if (item.Equals(itemId))
                {
                    return item;
                }
            }
            Debug.LogError($"Item with {itemId} was not found");
            return null;
        }
    }
}