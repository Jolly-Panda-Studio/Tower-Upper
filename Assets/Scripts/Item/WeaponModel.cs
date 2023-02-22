namespace Lindon.TowerUpper.Data
{
    public class WeaponModel : GameModel, IPurchasable
    {
        [UnityEngine.SerializeField] private ShopModel m_ShopModel;

        public ShopModel ShopModel => m_ShopModel;
    }
}