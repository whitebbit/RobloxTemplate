using System.Collections.Generic;
using _3._Scripts.Characters;
using _3._Scripts.Config;
using _3._Scripts.UI.Scriptable.Shop;
using _3._Scripts.Wallet;
using GBGamesPlugin;

namespace _3._Scripts.UI.Panels
{
    public class UpgradesShop: ShopPanel<UpgradeItem>
    {
        protected override IEnumerable<UpgradeItem> ShopItems()
        {
            return Configuration.Instance.AllUpgrades;
        }

        protected override bool ItemUnlocked(string id)
        {
            return GBGames.saves.upgradeSaves.Unlocked(id);
        }

        protected override bool IsSelected(string id)
        {
            return GBGames.saves.upgradeSaves.IsCurrent(id);
        }

        protected override void Select(string id)
        {
            if (!ItemUnlocked(id)) return;
            if (IsSelected(id)) return;
            
            GBGames.saves.upgradeSaves.SetCurrent(id);
            GBGames.instance.Save();
            Player.Player.Instance.UpgradeHandler.SetUpgrade(id);
            SetSlotsState();
        }

        protected override void Buy(string id)
        {
            if (ItemUnlocked(id)) return;

            var slot = GetSlot(id).Data;

            if (!WalletManager.TrySpend(slot.CurrencyType, slot.Price)) return;
            
            WalletManager.SpendByType(slot.CurrencyType, slot.Price);
            GBGames.saves.upgradeSaves.Unlock(id);
            Select(id);
        }
    }
}