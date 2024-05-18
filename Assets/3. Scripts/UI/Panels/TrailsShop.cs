﻿using System;
using System.Collections.Generic;
using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using _3._Scripts.UI.Elements;
using _3._Scripts.UI.Scriptable.Shop;
using _3._Scripts.Wallet;
using GBGamesPlugin;

namespace _3._Scripts.UI.Panels
{
    public class TrailsShop : ShopPanel<TrailItem>
    {
        protected override IEnumerable<TrailItem> ShopItems()
        {
            return Configuration.Instance.AllTrails;
        }

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
            if (!ItemUnlocked(id)) return;
            if (IsSelected(id)) return;
            
            GBGames.saves.trailSaves.SetCurrent(id);
            GBGames.instance.Save();
            Player.Player.Instance.TrailHandler.SetTrail(id);
            SetSlotsState();
        }

        protected override void Buy(string id)
        {
            if (ItemUnlocked(id)) return;

            var slot = GetSlot(id).Data;

            if (!WalletManager.TrySpend(slot.CurrencyType, slot.Price)) return;
            
            WalletManager.SpendByType(slot.CurrencyType, slot.Price);
            GBGames.saves.trailSaves.Unlock(id);
            Select(id);
        }

        protected override void OnSpawnItems(ShopSlot slot, TrailItem data)
        {
            slot.SetIconColor(data.Color);
        }
    }
}