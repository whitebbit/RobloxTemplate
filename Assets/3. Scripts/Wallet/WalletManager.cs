using System;
using _3._Scripts.Currency.Enums;
using GBGamesPlugin;

namespace _3._Scripts.Wallet
{
    public static class WalletManager
    {
        public static event Action<int ,int> OnFirstCurrencyChange;

        public static int FirstCurrency
        {
            get => GBGames.saves.walletSave.firstCurrency;
            set
            {
                OnFirstCurrencyChange?.Invoke(FirstCurrency, value);
                GBGames.saves.walletSave.firstCurrency = value;
            }
        }
        
        public static event Action<int ,int> OnSecondCurrencyChange;
        public static int SecondCurrency
        {
            get => GBGames.saves.walletSave.secondCurrency;
            set
            {
                OnSecondCurrencyChange?.Invoke(SecondCurrency, value);
                GBGames.saves.walletSave.secondCurrency = value;
            }
        }

        public static void SpendByType(CurrencyType currencyType, int count)
        {
            if(!TrySpend(currencyType, count)) return;

            switch (currencyType)
            {
                case CurrencyType.First:
                    FirstCurrency -= count;
                    break;
                case CurrencyType.Second:
                    SecondCurrency -= count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }
        public static bool TrySpend(CurrencyType currencyType, int count)
        {
            return currencyType switch
            {
                CurrencyType.First => FirstCurrency >= count,
                CurrencyType.Second => SecondCurrency >= count,
                _ => throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null)
            };
        }
    }
}