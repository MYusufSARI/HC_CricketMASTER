using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Menu, Bowler, Batsman, Wing, Lose, Draw }

public class GameManager : MonoBehaviour
{
    [Header(" Settings ")]
    private GameState gameState;
    private GameState firstGameState;


    private void Awake()
    {
        Application.targetFrameRate = 60;
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

        TryStartingNextGameMode();
    }


    private void TryStartingNextGameMode()
    {
        if (gameState == firstGameState)
        {
            // Trigger the first game mode
            if (firstGameState == GameState.Bowler)
                StartBowlerMode();
            else
                StartBatsmanMode();
        }

        else
        {
            // Trigger the Win / Draw / Lose
            Debug.LogWarning("Trigger Win / Lose / Draw Game Mode ! ");
        }
    }


    private void StartBowlerMode()
    {
        SceneManager.LoadScene("BowlerScene");
    }


    private void StartBatsmanMode()
    {
        SceneManager.LoadScene("BatsmanScene");
    }
}
