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
        private List<ShopModel> m_ShopModels;
        [SerializeField] private List<GameModel> m_Models;
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
            m_ShopModels = new List<ShopModel>();

            foreach (var model in m_Models)
            {
                if (model is IPurchasable purchasable)
                {
                    m_ShopModels.Add(purchasable.ShopModel);
                    m_Items.Add(purchasable.Data);
                }
            }
        }

        public List<ItemData> GetItems() => m_Items;

        public ItemCategory GetItemByCategory(int itemId)
        {
            var item = GetItemById(itemId);
            if (item == null)
            {
                return ItemCategory.None;
            }
            return item.Category;
        }

        public ItemData GetItemById(int itemId)
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

        #region Character

        public GameModel GetGameModel(int itemId)
        {
            foreach (var model in m_Models)
            {
                if (model.Equals(itemId))
                {
                    return model;
                }
            }
            Debug.LogError($"Game Model with {itemId} was not found");
            return null;
        }

        public ShopModel GetPreviewModel(int itemId)
        {
            foreach (var model in m_ShopModels)
            {
                if (model.Equals(itemId))
                {
                    return model;
                }
            }
            Debug.LogError($"Model with {itemId} was not found");
            return null;
        }

        #endregion

        #region Weapon

        #endregion

        #region Enemy

        public EnemyModel GetEnemyModel(int itemId)
        {
            foreach (var model in m_Models)
            {
                if (model.Equals(itemId) && model is EnemyModel enemy)
                {
                    return enemy;
                }
            }
            Debug.LogError($"Game Model with {itemId} was not found");
            return null;
        }

        #endregion

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