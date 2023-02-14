using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStarter 
{
    public static event Action OnStartGame;

    public static void StartGame()
    {
        OnStartGame?.Invoke();
    }
}
