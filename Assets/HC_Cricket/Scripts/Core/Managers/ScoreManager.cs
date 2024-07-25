using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header(" Settings ")]
    private int playerScore;
    private int aiScore;



    private void Start()
    {
        GameManager.onGameSet += ResetScores;

        LocalScoreManager.onScoreCalculated += ScoreCalculatedCallBack;
    }


    private void OnDestroy()
    {
        GameManager.onGameSet -= ResetScores;

        LocalScoreManager.onScoreCalculated -= ScoreCalculatedCallBack;
    }


    private void ScoreCalculatedCallBack(int score)
    {
        if (GameManager.instance.IsBowler())
        {
            aiScore += score;
        }

        else
        {
            playerScore += score;
        }

        Debug.Log("Player Score : " + playerScore);
        Debug.Log("Ai Score : " + aiScore);
    }


    public int GetPlayerScore()
    {
        return playerScore;
    }


    public int GetAiScore()
    {
        return aiScore;
    }


    public void ResetScores()
    {
        playerScore = 0;
        aiScore = 0;
    }
}
