using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Elements;
using _3._Scripts.UI.Panels.Base;
using _3._Scripts.UI.Scriptable.Shop;
using GBGamesPlugin;
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
                SetSlotsState(slot.ID, slot);
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
                SetSlotsState(item.ID, obj);

                _shopSlots.Add(obj);
            }
        }

        private void SetSlotsState(string id, ShopSlot obj)
        {
            if (!ItemUnlocked(id))
                obj.Lock();
            else
            {
                if (IsSelected(id))
                    obj.Select();
                else
                    obj.Unselect();
            }
        }

        private void OnClick(string id)
        {
            if (ItemUnlocked(id))
            {
                if (IsSelected(id)) return;
                
                Select(id);
                foreach (var slot in _shopSlots) SetSlotsState(slot.ID, slot);
                GBGames.instance.Save();
            }
            else
            {
                Buy(id);
                Select(id);
                foreach (var slot in _shopSlots) SetSlotsState(slot.ID, slot);
                GBGames.instance.Save();
            }
        }

        protected ShopSlot GetSlot(string id) => _shopSlots.FirstOrDefault(s => s.ID == id);
        protected abstract bool ItemUnlocked(string id);
        protected abstract bool IsSelected(string id);
        protected abstract void Select(string id);
        protected abstract void Buy(string id);
    }
}