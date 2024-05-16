using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Currency.Scriptable;
using _3._Scripts.UI.Scriptable.Shop;
using _3._Scripts.UI.Structs;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using VInspector;

namespace _3._Scripts.UI.Elements
{
    public class ShopSlot : MonoBehaviour
    {
        [Tab("Rarity Tables")] [SerializeField]
        private List<RarityTable> rarityTables = new();

        [Tab("UI")] [SerializeField] private Image glow;
        [SerializeField] private Image icon;
        [SerializeField] private Image table;
        [SerializeField] private Image backGlow;
        [Tab("Currency")] [SerializeField] private Image currencyIcon;
        [SerializeField] private TMP_Text price;
        [Tab("Localization")] [SerializeField] private string selectKey;
        [Tab("Localization")] [SerializeField] private string selectedKey;


        public string ID { get; private set; }

        public int Price { get; private set; }

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void SetView(ShopItem item, CurrencyData currencyData)
        {
            var rarity = rarityTables.FirstOrDefault(r => r.Rarity == item.Rarity);
            ID = item.ID;
            Price = item.Price;
            table.sprite = rarity.Table;
            glow.color = rarity.MainColor;
            backGlow.color = rarity.AdditionalColor;
            icon.sprite = item.Icon;
            currencyIcon.sprite = currencyData.Icon;
        }

        public void SetAction(Action action) => _button.onClick.AddListener(() => action?.Invoke());

        public void Select()
        {
            var word = LocalizationSettings.StringDatabase.GetTable("Localization").GetEntry(selectedKey)
                .GetLocalizedString();
            price.text = word;
            currencyIcon.gameObject.SetActive(false);
        }

        public void Unselect()
        {
            var word = LocalizationSettings.StringDatabase.GetTable("Localization").GetEntry(selectKey)
                .GetLocalizedString();
            price.text = word;
            currencyIcon.gameObject.SetActive(false);

        }

        public void Lock()
        {
            price.text = Price.ToString();
            currencyIcon.gameObject.SetActive(true);

        }
    }
}