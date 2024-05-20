﻿using System.Collections.Generic;
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
        [Tab("Data")]
        [SerializeField] private List<GiftItem> items = new();
        [Tab("UI")] 
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image gotImage;
        
        [Tab("Timer")]
        [SerializeField] private float timeToTake;
        [SerializeField]private Timer timer;
        private bool _rewarded;

        private GiftItem _currentItem;
        public void Initialize()
        {
            _currentItem = items[Random.Range(0, items.Count)];
            GetComponent<Button>().onClick.AddListener(GetReward);
            timer.StartTimer(timeToTake);
            timer.OnTimerEnd += () => timer.gameObject.SetActive(false);
            icon.sprite = _currentItem.Icon();
            title.text = _currentItem.Title();
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
            _currentItem.OnReward();
        }
    }
}