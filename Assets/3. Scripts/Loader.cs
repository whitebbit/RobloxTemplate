using System;
using System.Collections;
using System.Collections.Generic;
using GBGamesPlugin;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private void OnEnable()
    {
        GBGames.SaveLoadedCallback += Load;
    }

    private void OnDisable()
    {
        GBGames.SaveLoadedCallback -= Load;
    }

    private void Load()
    {
        SceneLoader.LoadScene(1);
    }
}
