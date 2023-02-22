using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Lindon/TowerUpper/Data/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private GameModelData m_Model;
        [SerializeField] private Sprite m_Icon;
        [SerializeField] private int m_Cost;
        [SerializeField] private ItemCategory m_Category;

        public int Id => m_Model.Id;
        public Sprite Icon => m_Icon;
        public int Cost => m_Cost;
        public ItemCategory Category => m_Category;


        public bool Equals(int id)
        {
            return Id == id;
        }

        public bool Equals(ItemCategory category)
        {
            return m_Category == category;
        }
    }

    public enum ItemCategory
    {
        None,
        Skin,
        Weapon
    }
}