using System;
using _3._Scripts.UI.Enums;
using UnityEngine;
using VInspector;

namespace _3._Scripts.UI.Scriptable.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Shop Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private string id;
        [Tab("UI")]
        [SerializeField] private Sprite icon;
        [SerializeField] private Rarity rarity;
        [SerializeField] private int price;

        public string ID => id;
        public Sprite Icon => icon;
        public Rarity Rarity => rarity;
        public int Price => price;
    }
}