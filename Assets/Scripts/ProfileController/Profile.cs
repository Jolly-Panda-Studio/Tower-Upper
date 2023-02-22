using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.GameController;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Profile
{
    public struct ChapeterData
    {
        public ChapeterData(int chapterId, int levelId)
        {
            ChapterLevel = chapterId;
            GameLevel = levelId;
        }

        public int ChapterLevel { get; private set; }
        public int GameLevel { get; private set; }
    }

    public static class ProfileExtention
    {
        public static ChapeterData GetChapter(this Profile profile)
        {
            var profileLevel = profile.Level;
            int chapterLevel = 0;
            if (profileLevel > 10)
            {
                chapterLevel = (profileLevel / 10);
            }

            var gameLevel = profileLevel - chapterLevel * 10;
            return new ChapeterData(chapterLevel, gameLevel);
        }
    }

    public class Profile
    {
        public string Token { get; private set; }
        public int Level { get; private set; }
        public int GoldAmount { get => GoldCalculator.GoldAmount; private set => GoldCalculator.GoldAmount = value; }
        private readonly List<int> m_ItemsId;

        private Dictionary<ItemCategory, int> m_ActiveItemsId;

        public event Action<int> OnAddItem;
        public event Action<int, ItemCategory> OnActiveItem;

        public Profile(string token, int level, int goldAmount)
        {
            Token = token;
            Level = level;
            GoldAmount = goldAmount;

            m_ItemsId = new List<int>();
            m_ActiveItemsId = new Dictionary<ItemCategory, int>();
            foreach (var key in System.Enum.GetValues(typeof(ItemCategory)))
            {
                m_ActiveItemsId.Add((ItemCategory)key, -1);
            }
        }

        public void SetActiveItem(int itemId, ItemCategory category)
        {
            if (!m_ActiveItemsId.ContainsKey(category))
            {
                Debug.LogWarning($"The {category} doesn't add to active item list!");
                return;
            }
            m_ActiveItemsId[category] = itemId;
            OnActiveItem?.Invoke(itemId, category);
        }

        public int GetActiveItem(ItemCategory category)
        {
            var itemId = m_ActiveItemsId[category];
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

        public void WinGame()
        {
            Level++;
        }
    }
}