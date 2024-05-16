using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Currency.Scriptable;
using _3._Scripts.Wallet;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    [CreateAssetMenu(fileName = "CurrencyRouletteItem", menuName = "Roulette Item/Currency", order = 0)]
    public class CurrencyRouletteItem : RouletteItem
    {
        [Tab("Base settings")]
        [SerializeField] private CurrencyType type;
        [SerializeField] private int count;
        [Tab("Data")]
        [SerializeField] private List<CurrencyData> currencyData;
        
        public override Sprite Icon()
        {
            return currencyData.FirstOrDefault(c => c.Type == type)?.Icon;
        }

        public override string Title()
        {
            return count.ToString();
        }

        public override void OnReward()
        {
            switch (type)
            {
                case CurrencyType.First:
                    WalletManager.FirstCurrency += count;
                    break;
                case CurrencyType.Second:
                    WalletManager.SecondCurrency += count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}