using _3._Scripts.UI.Scriptable.Roulette;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Elements
{
    public class RouletteSlot: MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        private RouletteItem _rouletteItem;
        
        public void Initialize(RouletteItem item)
        {
            _rouletteItem = item;
            icon.sprite = item.Icon();
            title.text = item.Title();
        }

        public void GetReward() => _rouletteItem.OnReward();

    }
}