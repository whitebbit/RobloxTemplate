﻿using _3._Scripts.Currency.Enums;
using UnityEngine;

namespace _3._Scripts.Currency.Scriptable
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "Currency Data", order = 0)]
    public class CurrencyData : ScriptableObject
    {
        [SerializeField] private CurrencyType type;
        [SerializeField] private Sprite icon;
        [SerializeField] private Color color;

        public CurrencyType Type => type;
        public Color Color => color;
        public Sprite Icon => icon;
    }
}