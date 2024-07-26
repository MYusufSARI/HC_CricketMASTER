using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum GameState { Menu, Bowler, Batsman, Wing, Lose, Draw }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header(" Settings ")]
    private GameState gameState;
    private GameState firstGameState;


    [Header(" Actions ")]
    public static Action onGameSet;



    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void PlayButtonCallback()
    {
        int randomStateIndex = Random.Range(0, 2);

        if (randomStateIndex == 0)
        {
            firstGameState = GameState.Batsman;
        }

        else
        {
            firstGameState = GameState.Bowler;
        }

        gameState = firstGameState;

        onGameSet?.Invoke();

        StartGame();
    }


    public void StartGame()
    {
        if (firstGameState == GameState.Bowler)
            StartBowlerMode();
        else
            StartBatsmanMode(); 
    }


    public void TryStartingNextGameMode()
    {
        if (gameState == firstGameState)
        {
            // Trigger the first game mode
            if (firstGameState == GameState.Bowler)
                StartBatsmanMode();
            else
                StartBowlerMode();
        }

        else
        {
            // Trigger the Win / Draw / Lose
            Debug.LogWarning("Trigger Win / Lose / Draw Game Mode ! ");

            FinishGame();
        }
    }


    private void StartBowlerMode()
    {
        gameState = GameState.Bowler;

        SceneManager.LoadScene("BowlerScene");
    }


    private void StartBatsmanMode()
    {
        gameState = GameState.Batsman;

        SceneManager.LoadScene("BatsmanScene");
    }


    private void FinishGame()
    {
        if (ScoreManager.instance.IsPlayerWin())
        {
            // Set the Win state
        }

        else if (ScoreManager.instance.IsPlayerLose())
        {
            // Set the Lose state
        }

        else
        {
            // Set the Draw state
        }
    }


    public bool IsBowler()
    {
        return gameState == GameState.Bowler;
    }
}
