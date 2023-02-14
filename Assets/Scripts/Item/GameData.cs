using Lindon.TowerUpper.GameController.Level;
using Lindon.TowerUpper.Initilizer;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class GameData : MonoBehaviour, IInitilizer
    {
        public static GameData Instance { get; private set; }

        private List<ItemData> m_Items;
        [SerializeField] private List<ItemModel> m_Models;
        [SerializeField] private List<ChapterData> m_Chapters;
        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;

                LoadItems();

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Item

        #region Data

        private void LoadItems()
        {
            m_Items = new List<ItemData>();
            foreach (var model in m_Models)
            {
                m_Items.Add(model.Data);
            }
        }

        public List<ItemData> GetItems() => m_Items;

        public ItemCategory GetItemCategory(int itemId)
        {
            var item = GetItem(itemId);
            if (item == null)
            {
                Debug.LogError($"Item with {itemId} was not found");
                return ItemCategory.None;
            }
            return item.Category;
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
        #endregion

        #region Model

        public SubModel GetModel(int itemId)
        {
            foreach (var model in m_Models)
            {
                if (model.Equals(itemId))
                {
                    return model.GetSubModel();
                }
            }
            Debug.LogError($"Model with {itemId} was not found");
            return null;

        }
        #endregion

        #endregion

        public GameInfo GetGameInfo(int chapterLevel, int gameLevel)
        {
            foreach (var chapter in m_Chapters)
            {
                if (chapter.Equals(chapterLevel))
                {
                    return chapter.GetGame(gameLevel);
                }
            }
            Debug.LogError($"Game with {chapterLevel} was not found");
            return null;
        }
    }
}