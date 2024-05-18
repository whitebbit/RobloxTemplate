using UnityEngine;

namespace _3._Scripts.UI.Scriptable.Shop
{
    [CreateAssetMenu(fileName = "CharacterShopItem", menuName = "Shop Items/Character Item", order = 0)]
    public class CharacterItem : ShopItem
    {
        public override string Title()
        {
            return "";
        }
    }
}