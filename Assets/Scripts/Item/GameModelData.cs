using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    [CreateAssetMenu(fileName = "GameModel", menuName = "Lindon/TowerUpper/Data/GameModel")]
    public class GameModelData : ScriptableObject
    {
        [SerializeField] private int m_Id;

        public int Id => m_Id;

        public bool Equals(int id)
        {
            return Id == id;
        }
    }
}