using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Pets.Scriptables;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Pets
{
    public class PetUnlocker : MonoBehaviour
    {
        [SerializeField] private List<PetData> data = new();

        public void UnlockRandom()
        {
            var list = data.Where(p => !GBGames.saves.petSaves.Unlocked(p.ID)).ToList();
            if(list.Count <= 0) return;
            
            var rand = list[Random.Range(0, list.Count)];
            if(rand == null) return;
            
            GBGames.saves.petSaves.Unlock(rand.ID);
            GBGames.instance.Save();
        }
    }
}