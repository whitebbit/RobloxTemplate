﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GBGamesPlugin
{
    [CreateAssetMenu(menuName = "GBGamesSettings", fileName = "GBGamesSettings")]
    public class GBGamesSettings : ScriptableObject
    {
        [Header("Editor")] [Tooltip("Вывод отладочных сообщений в консоль")]
        public bool debug = true;

        [Header("Advertisement")] [Tooltip("Минимальный интервал между показами межстраничной рекламы")]
        public int minimumDelayBetweenInterstitial = 60;

        [Tooltip("Если платформа поддерживает баннер, включить ли его при старте игры")]
        public bool enableBannerAutomatically = true;
        
        
        [Header("Storage")]
        [Tooltip("Будет ли сохранения загружаться в облако. Не все платформы поддерживают облачные сохранения")]
        public bool useCloudSaves = true;
        [Tooltip("Использовать переодическое сохранение")]
        public bool autoSaveByInterval = true;
        [ConditionallyVisible(nameof(autoSaveByInterval))] [Range(5, 100)]
        [Tooltip("Интервал переодических сохранений в минутах")]
        public int saveInterval;
        [Tooltip("Использовать переодическое сохранение")]
        public bool saveOnChangeVisibilityState = true;
        
        [Header("Player")]
        [Tooltip("Вызвать ли окно аутентификации  при старте")]
        public bool authPlayerAutomatically = true;

        [Header("Yandex")] [Tooltip("Запросить у игрока доступ к имени и фото")]
        public bool yandexScopes = true;

        [Tooltip("Автоматически вызывать GRA")]
        public bool autoGameReadyAPI;

        [Header("Crazy Games")] public bool gameLoadingCallbacksOnSceneLoading = true;
    }
}