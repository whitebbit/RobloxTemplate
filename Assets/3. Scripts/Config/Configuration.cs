﻿using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Currency.Scriptable;
using _3._Scripts.Pets.Scriptables;
using _3._Scripts.Singleton;
using _3._Scripts.UI.Scriptable.Shop;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Config
{
    public class Configuration : Singleton<Configuration>
    {
        [SerializeField] private List<CurrencyData> currencyData = new();
        [SerializeField] private List<CharacterItem> allCharacters = new();
        [SerializeField] private List<TrailItem> allTrails = new();
        [SerializeField] private List<PetData> allPets = new();

        public IEnumerable<PetData> AllPets => allPets;

        public IEnumerable<CharacterItem> AllCharacters => allCharacters;

        public IEnumerable<TrailItem> AllTrails => allTrails;
        public CurrencyData GetCurrency(CurrencyType type) => currencyData.FirstOrDefault(c => c.Type == type);
    }
}