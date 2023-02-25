using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class WeaponModel : GameModel, IPurchasable
    {
        [UnityEngine.SerializeField] private ShopModel m_ShopModel;

        public ShopModel ShopModel => m_ShopModel;

        private void Start()
        {
            var armory = GetComponentInParent<Armory>();
            if(armory == null)
            {
                Debug.Log("No Armory found!", gameObject);
            }
            armory.AddWeapon(this);
        }
    }
}