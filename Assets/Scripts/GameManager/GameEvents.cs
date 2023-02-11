using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnStartGame;

    public static bool GameStarted { get; private set; } = false;

    public static void StartGame()
    {
        GameStarted = true;
    }

    public static void PauseGame()
    {
        GameStarted = false;
    }

    public static void StopGame()
    {
        GameStarted = false;
    }
}
