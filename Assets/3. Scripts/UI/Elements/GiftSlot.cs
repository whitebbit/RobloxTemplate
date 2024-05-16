using _3._Scripts.UI.Scriptable.Roulette;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

namespace _3._Scripts.UI.Elements
{
    public class GiftSlot : MonoBehaviour
    {
        [Tab("Data")] [SerializeField] private GiftItem item;
        [Tab("UI")] 
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image gotImage;
        
        [Tab("Timer")]
        [SerializeField] private float timeToTake;
        [SerializeField]private Timer timer;
        private bool _rewarded;
        
        public void Initialize()
        {
            GetComponent<Button>().onClick.AddListener(GetReward);
            timer.StartTimer(timeToTake);
            timer.OnTimerEnd += () => timer.gameObject.SetActive(false);
            icon.sprite = item.Icon();
            title.text = item.Title();
            gotImage.DOFade(0, 0);
            gotImage.gameObject.SetActive(false);
        }

        private void GetReward()
        {
            if (!timer.TimerEnd()) return;
            if(_rewarded) return;
            
            gotImage.gameObject.SetActive(true);
            gotImage.DOFade(1, 0.15f);
            _rewarded = true;
            item.OnReward();
        }
    }
}