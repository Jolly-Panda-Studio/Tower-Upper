using Lindon.TowerUpper.Data;
using Lindon.TowerUpper.Profile;

namespace Lindon.TowerUpper.GameController
{
    public static class ShopController
    {
        private static bool CanBuy(ItemData item)
        {
            var controller = ProfileController.Instance;
            return controller.Profile.GoldAmount >= item.Cost;
        }

        public static bool Buy(ItemData item)
        {
            var controller = ProfileController.Instance;
            if (CanBuy(item))
            {
                controller.Profile.BuyItem(item.Id, item.Cost);
                return true;
            }
            return false;
        }
    }
}