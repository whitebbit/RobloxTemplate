using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Config;
using _3._Scripts.Pets.Scriptables;
using _3._Scripts.UI.Elements;
using _3._Scripts.UI.Panels.Base;
using GBGamesPlugin;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

namespace _3._Scripts.UI.Panels
{
    public class PetsPanel : SimplePanel
    {
        [Tab("Slots")] [SerializeField] private PetSlot prefab;
        [SerializeField] private Transform container;
        [Tab("Selected")] [SerializeField] private PetSlot selected;
        [SerializeField] private PetBooster booster;
        [Tab("Buttons")] [SerializeField] private Button select;
        [SerializeField] private Button unselect;

        private PetSlot _currentSlot;
        private List<PetSlot> _slots = new();
        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;

            booster.gameObject.SetActive(false);
            select.onClick.AddListener(Select);
            unselect.onClick.AddListener(Unselect);
        }

        protected override void OnOpen()
        {
            InitializeSlots();
        }

        protected override void OnClose()
        {
            DeleteSlots();
        }

        private void InitializeSlots()
        {
            var unlocked = GBGames.saves.petSaves.unlocked;
            foreach (var item in unlocked)
            {
                var data = Configuration.Instance.AllPets.FirstOrDefault(d => d.ID == item);
                var obj = Instantiate(prefab, container);
                obj.Initialize(data);
                obj.SetAction(OnClick);
                _slots.Add(obj);
            }
        }

        private void DeleteSlots()
        {
            foreach (var child in _slots)
            {
                Destroy(child.gameObject);
            }
            _slots.Clear();
        }

        private void OnClick(PetSlot slot)
        {
            _currentSlot = slot;

            booster.gameObject.SetActive(true);
            selected.Initialize(slot.Data);
            booster.SetBooster(slot.Data);
        }

        private void Select()
        {
            if (_currentSlot == null) return;

            var data = _currentSlot.Data;

            if (GBGames.saves.petSaves.FilledIn(3)) return;
            if (GBGames.saves.petSaves.IsCurrent(data.ID)) return;

            _currentSlot.Select();
            var player = Player.Player.Instance.transform;
            var position = player.position + player.right * 2;
            Player.Player.Instance.PetsHandler.CreatePet(data, position);

            GBGames.saves.petSaves.SetCurrent(data.ID);
            GBGames.instance.Save();
        }


        private void Unselect()
        {
            if (_currentSlot == null) return;

            var data = _currentSlot.Data;

            if (!GBGames.saves.petSaves.IsCurrent(data.ID)) return;

            _currentSlot.Unselect();
            Player.Player.Instance.PetsHandler.DestroyPet(data.ID);
            GBGames.saves.petSaves.RemoveCurrent(data.ID);
            GBGames.instance.Save();
        }
    }
}