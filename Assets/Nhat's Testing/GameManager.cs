using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Play:
                break;
            case GameState.Win:
                WinGame();
                break;
            case GameState.Lose:
                LoseGame();
                break;
        }
    }

    private void LoseGame()
    {
        throw new NotImplementedException();
    }

    private void WinGame()
    {
        throw new NotImplementedException();
    }

    public enum GameState
{
    Play,
    Win,
    Lose
}
    public static int PlayerHealth { get; set; }
}