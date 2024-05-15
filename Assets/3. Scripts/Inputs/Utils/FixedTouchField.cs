﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _3._Scripts.Inputs.Utils
{
    public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private int _pointerId;
        private Vector2 _pointerOld;

        public Vector2 Axis { get; private set; }
        public bool Pressed { get; private set; }
        private void Update()
        {
            if (Pressed)
            {
                if (_pointerId >= 0 && _pointerId < Input.touches.Length)
                {
                    Axis = Input.touches[_pointerId].position - _pointerOld;
                    _pointerOld = Input.touches[_pointerId].position;
                }
                else
                {
                    Axis = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                    _pointerOld = Input.mousePosition;
                }
            }
            else
            {
                Axis = new Vector2();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            _pointerId = eventData.pointerId;
            _pointerOld = eventData.position;
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
    }
}