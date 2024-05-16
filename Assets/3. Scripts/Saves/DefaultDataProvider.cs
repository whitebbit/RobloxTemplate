using System;
using _3._Scripts.UI.Scriptable.Shop;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Saves
{
    public class DefaultDataProvider : MonoBehaviour
    {
        [SerializeField] private ShopItem defaultCharacter;
        [SerializeField] private ShopItem defaultTrail;

        private void OnEnable()
        {
            GBGames.SaveLoadedCallback += SetDefault;
        }
    
    
        private void OnDisable()
        {
            GBGames.SaveLoadedCallback -= SetDefault;
        }



        private void SetDefault()
        {
            if (GBGames.saves.defaultLoaded) return;
            
            GBGames.saves.characterSaves.current = defaultCharacter.ID;
            GBGames.saves.characterSaves.Unlock(defaultCharacter.ID);
            GBGames.saves.trailSaves.current = defaultTrail.ID;
            GBGames.saves.trailSaves.Unlock(defaultTrail.ID);
            
            GBGames.saves.defaultLoaded = true;
            GBGames.instance.Save();
        }
    }
}