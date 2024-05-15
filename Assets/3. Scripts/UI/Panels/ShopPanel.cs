using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Elements;
using _3._Scripts.UI.Panels.Base;
using _3._Scripts.UI.Scriptable.Shop;
using GBGamesPlugin;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace _3._Scripts.UI.Panels
{
    public abstract class ShopPanel : SimplePanel
    {
        [SerializeField] private List<ShopItem> shopItems = new();
        [SerializeField] private Transform container;

        [SerializeField] private ShopSlot prefab;

        private readonly List<ShopSlot> _shopSlots = new();

        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
            SpawnItems();
        }

        protected override void OnOpen()
        {
            InitializeItems();
        }

        private void InitializeItems()
        {
            foreach (var slot in _shopSlots)
            {
                if (!ItemUnlocked(slot.ID))
                    slot.Lock();
                else
                {
                    if (GBGames.saves.characterSaves.IsCurrentCharacter(slot.ID))
                        slot.Select();
                    else
                        slot.Unselect();
                }
            }
        }
        
        private void SpawnItems()
        {
            var items = shopItems.OrderBy(obj => obj.Price).ToList();
            foreach (var item in items)
            {
                var obj = Instantiate(prefab, container);
                obj.SetView(item);
                obj.SetAction(() => OnClick(item.ID));
                if (!ItemUnlocked(item.ID))
                    obj.Lock();
                else
                {
                    if (GBGames.saves.characterSaves.IsCurrentCharacter(item.ID))
                        obj.Select();
                    else
                        obj.Unselect();
                }

                _shopSlots.Add(obj);
            }
        }

        private void OnClick(string id)
        {
            if (ItemUnlocked(id))
            {
                foreach (var slot in _shopSlots) slot.Unselect();
                Select(id);
                GetSlot(id).Select();
            }
            else
            {
                Buy(id);
                foreach (var slot in _shopSlots) slot.Unselect();
                GetSlot(id).Select();
            }
        }

        protected ShopSlot GetSlot(string id) => _shopSlots.FirstOrDefault(s => s.ID == id);
        protected abstract bool ItemUnlocked(string id);
        protected abstract void Select(string id);
        protected abstract void Buy(string id);
    }
}