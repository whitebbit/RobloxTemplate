using System;
using _3._Scripts.Inputs;
using _3._Scripts.Inputs.Interfaces;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private float cooldownTime;
        public event Action Action;

        private IInput _input;
        private bool _isOnCooldown;
        private float _cooldownTimer;
        private PlayerAnimator _animator;

        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();
        }

        private void Start()
        {
            _input = InputHandler.Instance.Input;
        }

        private void Update()
        {
            if (_input.GetAction()) DoAction();
            
            Cooldown();
        }

        private void DoAction()
        {
            if (_isOnCooldown) return;
            _isOnCooldown = true;
            _cooldownTimer = cooldownTime;
            _animator.DoAction();
            Action?.Invoke(); 
        }

        private void Cooldown()
        {
            if (!_isOnCooldown) return;
            _cooldownTimer -= Time.deltaTime;
            if (!(_cooldownTimer <= 0f)) return;
            _isOnCooldown = false;
        }
    }
}