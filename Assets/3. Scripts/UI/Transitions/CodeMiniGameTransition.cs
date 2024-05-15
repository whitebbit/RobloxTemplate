using System;
using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI.Transitions
{
    [Serializable]
    public class CodeMiniGameTransition: IUITransition 
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform body;
        
        [Space] 
        [SerializeField] private Vector3 activePosition;
        [SerializeField] private Vector3 inactivePosition;
        [Space]
        [SerializeField] private float duration;
        
        public Tween AnimateIn()
        {
            canvasGroup.blocksRaycasts = true;
            body.DOLocalMove(activePosition, duration).SetEase(Ease.OutBack);
            return canvasGroup.DOFade(1, duration).SetLink(canvasGroup.gameObject);
        }

        public Tween AnimateOut()
        {
            canvasGroup.blocksRaycasts = false;
            body.DOLocalMove(inactivePosition, duration).SetEase(Ease.InBack);
            return canvasGroup.DOFade(0, duration).SetLink(canvasGroup.gameObject);
        }

        public void ForceIn()
        {
            body.localPosition = activePosition;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }

        public void ForceOut()
        {
            body.localPosition = inactivePosition;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
}