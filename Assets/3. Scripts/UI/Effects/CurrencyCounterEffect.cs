using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Effects
{
    public class CurrencyCounterEffect : UIEffect
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TMP_Text counter;
        [SerializeField] private Image icon;


        public void Initialize(CurrencyType type, float count)
        {
            var image = Configuration.Instance.GetCurrency(type).Icon;
            var rect = transform as RectTransform;
            icon.sprite = image;
            counter.text = $"+{count}";

            canvasGroup.alpha = 0;
            if (rect is not null) 
                transform.DOMoveY(transform.position.y + rect.sizeDelta.y, 1.5f).SetLink(gameObject);
            canvasGroup.DOFade(1, 0.25f).SetLink(gameObject);
            canvasGroup.DOFade(0, 0.25f).SetDelay(1).SetLink(gameObject).OnComplete(() => Destroy(gameObject));
        }
    }
}