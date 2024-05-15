using System;
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
    }
}