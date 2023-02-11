using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.GameController;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Profile
{
    public class Profile
    {
        public string Token { get; private set; }
        public int Level { get; private set; }
        public int GoldAmount { get => GoldCalculator.GoldAmount; private set => GoldCalculator.GoldAmount = value; }
        private readonly List<int> m_ItemsId;

        public Dictionary<ItemCategory, int> ActiveItemsId;

        public event Action<int> OnAddItem;
        public event Action<int,ItemCategory> OnActiveItem;

        public Profile(string token, int level, int goldAmount)
        {
            Token = token;
            Level = level;
            GoldAmount = goldAmount;

            m_ItemsId = new List<int>();
            ActiveItemsId = new Dictionary<ItemCategory, int>();
            foreach (var key in System.Enum.GetValues(typeof(ItemCategory)))
            {
                ActiveItemsId.Add((ItemCategory)key, -1);
            }

            ActiveItemsId[ItemCategory.Skin] = 102;
        }

        public void SetActiveItem(int itemId, ItemCategory category)
        {
            if (!ActiveItemsId.ContainsKey(category))
            {
                Debug.LogWarning($"The {category} doesn't add to active item list!");
                return;
            }
            ActiveItemsId[category] = itemId;
            OnActiveItem?.Invoke(itemId,category);
        }

        public int GetActiveItem(ItemCategory category)
        {
            var itemId = ActiveItemsId[category];
            if (itemId == -1)
            {
                Debug.LogWarning($"There are no active items in {category}!");
            }
            return itemId;
        }

        public void BuyItem(int itemId, int itemCost)
        {
            m_ItemsId.Add(itemId);
            OnAddItem?.Invoke(itemId);
            GoldAmount -= itemCost;
        }

        public int GetItem(int itemId)
        {
            foreach (var id in m_ItemsId)
            {
                if (id == itemId)
                {
                    return id;
                }
            }
            Debug.LogError($"Item with {itemId} was not found");
            return -1;
        }

        public List<int> GetItems() => m_ItemsId;
    }
}