namespace Lindon.TowerUpper.Data
{
    public interface IPurchasable
    {
        public ShopModel ShopModel { get; }
        public ItemData Data => ShopModel.Data;
    }
}