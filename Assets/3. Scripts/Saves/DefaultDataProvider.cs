using System;
using _3._Scripts.UI.Scriptable.Shop;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Saves
{
    public class DefaultDataProvider : MonoBehaviour
    {
        [SerializeField] private ShopItem defaultCharacter;

        private void OnEnable()
        {
            GBGames.SaveLoadedCallback += LoadComplete;
        }

        private void OnDisable()
        {
            GBGames.SaveLoadedCallback -= LoadComplete;
        }

        private void LoadComplete()
        {
            if (!string.IsNullOrEmpty(GBGames.saves.characterSaves.currentCharacter)) return;
            
            GBGames.saves.characterSaves.currentCharacter = defaultCharacter.ID;
            GBGames.saves.characterSaves.UnlockCharacter(defaultCharacter.ID);
        }
    }
}