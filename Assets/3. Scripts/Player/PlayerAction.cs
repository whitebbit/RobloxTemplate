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

        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void Start()
        {
            Action += () => { Debug.Log("Action"); };
        }

        private void Update()
        {
            if (_input.GetAction()) Action?.Invoke();
        }
    }
}