using System;
using _3._Scripts.Inputs;
using _3._Scripts.Inputs.Interfaces;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class PlayerAction : MonoBehaviour
    {
        public event Action Action;

        private IInput _input;

        private void Start()
        {
            _input = InputHandler.Instance.Input;
        }
        
        private void Update()
        {
            if (_input.GetAction()) Action?.Invoke();
        }
    }
}