using System;
using System.Linq;
using _3._Scripts.Boosters;
using _3._Scripts.Config;
using _3._Scripts.Inputs;
using _3._Scripts.Inputs.Interfaces;
using _3._Scripts.Wallet;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class PlayerAction : MonoBehaviour
    {
        
        [SerializeField] private float baseCooldownTime;
        [SerializeField] private AnimationClip actionAnimation;
        
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
            if (_input.GetAction() || BoostersHandler.Instance.UseAutoClicker) DoAction();
            Cooldown();
        }

        private void DoAction()
        {
            if (_isOnCooldown) return;
            _isOnCooldown = true;
            _cooldownTimer = GetCooldown();
            _animator.DoAction(GetActionSpeed());
            Action?.Invoke();
        }
        
        private void Cooldown()
        {
            if (!_isOnCooldown) return;
            _cooldownTimer -= Time.deltaTime;
            if (!(_cooldownTimer <= 0f)) return;
            _isOnCooldown = false;
        }

        private float GetActionSpeed()
        {
            return actionAnimation.length / GetCooldown();
        }
        
        private float GetCooldown()
        {
            var booster =
                Configuration.Instance.AllUpgrades.FirstOrDefault(u => GBGames.saves.upgradeSaves.IsCurrent(u.ID)).Booster;
            return Mathf.Clamp(baseCooldownTime * booster, 0.25f, 10);
        }
    }
}