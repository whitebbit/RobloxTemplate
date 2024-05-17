using System;
using System.Linq;
using _3._Scripts.Config;
using _3._Scripts.Pets;
using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class Player : Singleton<Player>
    {
        public PetsHandler PetsHandler { get; private set; }

        private void Awake()
        {
            PetsHandler = new PetsHandler();
        }

        private void Start()
        {
            GBGames.SaveLoadedCallback += InitializePets;
        }

        private void InitializePets()
        {
            foreach (var data in GBGames.saves.petSaves.current.Select(id =>
                Configuration.Instance.AllPets.FirstOrDefault(p => p.ID == id)))
            {
                PetsHandler.CreatePet(data);
            }
        }
    }
}