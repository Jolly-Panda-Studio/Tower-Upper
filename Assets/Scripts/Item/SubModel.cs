using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class SubModel : MonoBehaviour
    {
        [SerializeField] private Mode m_Mode;

        public bool Equals(Mode mode) => m_Mode == mode;    

        public enum Mode
        {
            Player,
            Enemy,
            Weapon
        }
    }
}