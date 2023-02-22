using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public abstract class GameModel : MonoBehaviour
    {
        [SerializeField] private GameModelData m_Data;

        public int Id => m_Data.Id;

        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}