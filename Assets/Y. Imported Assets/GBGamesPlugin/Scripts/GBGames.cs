using System;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GBGamesPlugin
{
    public partial class GBGames : MonoBehaviour
    {
        public GBGamesSettings settings;
        public static GBGames instance { get; private set; }

        private void Awake()
        {
            Singleton();
            Storage();
            Advertisement();
            Platform();
            Player();
            Game();
        }

        private void Singleton()
        {
            transform.SetParent(null);
            gameObject.name = "GBGames";

            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        private void Advertisement()
        {
            Bridge.advertisement.bannerStateChanged += OnBannerStateChanged;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;

            minimumDelayBetweenInterstitial = instance.settings.minimumDelayBetweenInterstitial;

            if (instance.settings.enableBannerAutomatically)
                ShowBanner();

            if (instance.settings.saveOnChangeVisibilityState)
                GameHiddenStateCallback += Save;
        }

        private void Platform()
        {
            if (instance.settings.autoGameReadyAPI)
                GameReady();

            if (instance.settings.gameLoadingCallbacksOnSceneLoading)
            {
                SceneManager.sceneLoaded += (_, _) => { InGameLoadingStopped(); };
            }
        }

        private void Player()
        {
            if (instance.settings.authPlayerAutomatically)
                AuthorizePlayer();
        }

        private void Storage()
        {
            Load();
            if (instance.settings.autoSaveByInterval)
                StartCoroutine(IntervalSave());
        }

        private void Game()
        {
            Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;
        }
    }
}