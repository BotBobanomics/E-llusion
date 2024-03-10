using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play,
        Pause,
        Win,
        Lose
    }

    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> StateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        State = GameState.Play;
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Play:
                PlayGame();
                break;
            case GameState.Pause:
                Debug.Log("pausing");
                PauseGame();
                break;
            case GameState.Win:
                WinGame();
                break;
            case GameState.Lose:
                LoseGame();
                break;
            default:
                Debug.Log("ERROR: Unknown game state: " + newState);
                break;
        }
        StateChanged?.Invoke(newState);
    }

    private void PlayGame()
    {
        Time.timeScale = 1f;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void LoseGame()
    {
        throw new NotImplementedException();
    }

    private void WinGame()
    {
        throw new NotImplementedException();
    }

    public static int PlayerHealth { get; set; }
}