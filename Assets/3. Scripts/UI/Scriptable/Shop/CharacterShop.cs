using _3._Scripts.Saves;
using _3._Scripts.UI.Panels;
using _3._Scripts.Wallet;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.UI.Scriptable.Shop
{
    public class CharacterShop : ShopPanel
    {
        protected override bool ItemUnlocked(string id)
        {
            return GBGames.saves.characterSaves.CharacterUnlocked(id);
        }

        protected override void Select(string id)
        {
            if (GBGames.saves.characterSaves.IsCurrentCharacter(id)) return;

            GBGames.saves.characterSaves.SetCurrentCharacter(id);
            GBGames.instance.Save();
        }

        protected override void Buy(string id)
        {
            if (ItemUnlocked(id)) return;
            if (WalletManager.FirstCurrency < GetSlot(id).Price) return;

            WalletManager.FirstCurrency -= GetSlot(id).Price;
            GBGames.saves.characterSaves.UnlockCharacter(id);
            
            GBGames.instance.Save();
        }
    }
}