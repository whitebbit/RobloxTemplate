using _3._Scripts.UI.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    public abstract class RouletteItem: ScriptableObject
    {
        
        public abstract void OnReward();
    }
}