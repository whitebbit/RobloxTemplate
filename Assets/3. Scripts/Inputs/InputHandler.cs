using System;
using _3._Scripts.Inputs.Interfaces;
using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;
using DeviceType = InstantGamesBridge.Modules.Device.DeviceType;

namespace _3._Scripts.Inputs
{
    public class InputHandler : Singleton<InputHandler>
    {
        [SerializeField] private MobileInput mobileInput;
        private DesktopInput _desktopInput;

        public IInput Input
        {
            get
            {
                switch (GBGames.deviceType)
                {
                    case DeviceType.Mobile:
                        if(!mobileInput.gameObject.activeSelf)
                            mobileInput.gameObject.SetActive(true);
                        return mobileInput;
                    case DeviceType.Desktop:
                        if(!mobileInput.gameObject.activeSelf)
                            mobileInput.gameObject.SetActive(true);
                        return mobileInput;
                    case DeviceType.Tablet:
                        return default;
                    case DeviceType.TV:
                        return default;
                    default: 
                        return default;
                }
            }
        }

        protected override void OnAwake()
        {
            _desktopInput = new DesktopInput();
        }
    }
}