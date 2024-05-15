﻿using UnityEngine;

namespace GBGamesPlugin
{
    public static class PauseController
    {
        private static bool _audioPause;
        private static float _timeScale;
        private static CursorLockMode _cursorLockMode;
        private static bool _cursorVisible;

        public static void Pause(bool state)
        {
            if (state)
            {
                _audioPause = AudioListener.pause;
                _timeScale = Time.timeScale;
                _cursorLockMode = Cursor.lockState;
                _cursorVisible = Cursor.visible;

                AudioListener.pause = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = _timeScale;
                AudioListener.pause = _audioPause;
                Cursor.lockState = _cursorLockMode;
                Cursor.visible = _cursorVisible;
            }
        }
    }
}