using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public abstract class GameModel : MonoBehaviour
    {
        [SerializeField, AssetPopup(typeof(GameModelData))] private GameModelData m_Data;

        public int Id => m_Data.Id;

        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}