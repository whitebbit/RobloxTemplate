using _3._Scripts.Wallet;
using GBGamesPlugin;

namespace _3._Scripts.UI.Panels
{
    public class TrailsShop : ShopPanel
    {
        protected override bool ItemUnlocked(string id)
        {
            return GBGames.saves.trailSaves.Unlocked(id);
        }

        protected override bool IsSelected(string id)
        {
            return GBGames.saves.trailSaves.IsCurrent(id);
        }

        protected override void Select(string id)
        {
            if (IsSelected(id)) return;

            GBGames.saves.trailSaves.SetCurrent(id);
        }

        protected override void Buy(string id)
        {
            if (ItemUnlocked(id)) return;
            if (WalletManager.FirstCurrency < GetSlot(id).Price) return;

            WalletManager.FirstCurrency -= GetSlot(id).Price;
            GBGames.saves.trailSaves.Unlock(id);
        }
    }
}