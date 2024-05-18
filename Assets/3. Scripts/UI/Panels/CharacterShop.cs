using System.Collections.Generic;
using _3._Scripts.Config;
using _3._Scripts.UI.Scriptable.Shop;
using _3._Scripts.Wallet;
using GBGamesPlugin;

namespace _3._Scripts.UI.Panels
{
    public class CharacterShop : ShopPanel<CharacterItem>
    {
        protected override IEnumerable<CharacterItem> ShopItems()
        {
            return Configuration.Instance.AllSkins;
        }

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
            if (!ItemUnlocked(id)) return;
            if (IsSelected(id)) return;

            GBGames.saves.characterSaves.SetCurrent(id);
            SetSlotsState();
            GBGames.instance.Save();
        }

        protected override void Buy(string id)
        { 
            if (ItemUnlocked(id)) return;

            var slot = GetSlot(id).Data;

            if (!WalletManager.TrySpend(slot.CurrencyType, slot.Price)) return;
            
            WalletManager.SpendByType(slot.CurrencyType, slot.Price);
            GBGames.saves.characterSaves.Unlock(id);
            Select(id);
        }
    }
}