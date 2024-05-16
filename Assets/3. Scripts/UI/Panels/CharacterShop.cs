using _3._Scripts.Wallet;
using GBGamesPlugin;

namespace _3._Scripts.UI.Panels
{
    public class CharacterShop : ShopPanel
    {
        protected override bool ItemUnlocked(string id)
        {
            return GBGames.saves.characterSaves.Unlocked(id);
        }

        protected override bool IsSelected(string id)
        {
            return GBGames.saves.characterSaves.IsCurrent(id);
        }

        protected override void Select(string id)
        {
            if (IsSelected(id)) return;

            GBGames.saves.characterSaves.SetCurrent(id);
        }

        protected override void Buy(string id)
        {
            if (ItemUnlocked(id)) return;
            if (WalletManager.FirstCurrency < GetSlot(id).Price) return;

            WalletManager.FirstCurrency -= GetSlot(id).Price;
            GBGames.saves.characterSaves.Unlock(id);
        }
    }
}