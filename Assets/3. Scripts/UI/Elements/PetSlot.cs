﻿using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Pets.Scriptables;
using _3._Scripts.UI.Structs;
using DG.Tweening;
using GBGamesPlugin;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

namespace _3._Scripts.UI.Elements
{
    public class PetSlot : MonoBehaviour
    {
        [Tab("Rarity Tables")] [SerializeField]
        private List<RarityTable> rarityTables = new();

        [Tab("UI")] [SerializeField] private Image icon;
        [SerializeField] private Image table;
        [SerializeField] private Image selected;

        public PetData Data { get; private set; }
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(PetData data)
        {
            var rarity = rarityTables.FirstOrDefault(p => p.Rarity == data.Rarity);
            var select = GBGames.saves.petSaves.IsCurrent(data.ID);
            Data = data;
            icon.gameObject.SetActive(true);
            selected.DOFade(select ? 1 : 0, 0);
            icon.sprite = data.Icon;
            table.sprite = rarity.Table;
        }

        public void SetAction(Action<PetSlot> action)
        {
            _button.onClick.AddListener(() => action?.Invoke(this));
        }

        public void Select()
        {
            selected.DOFade(1, 0);
        }

        public void Unselect()
        {
            selected.DOFade(0, 0);
        }
    }
}