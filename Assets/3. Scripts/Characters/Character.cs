using System;
using UnityEngine;

namespace _3._Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Player.Player.Instance.Animator.SetAnimator(_animator);
        }
    }
}