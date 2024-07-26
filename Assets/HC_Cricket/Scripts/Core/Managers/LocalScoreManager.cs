using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalScoreManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI[] scoreTexts;


    [Header(" Settings ")]
    [SerializeField] private float firstRingRadius;
    [SerializeField] private float secondRingRadius;
    private int currentOver;


    [Header(" Events ")]
    public static Action<int> onScoreCalculated;


    private void Start()
    {
        ClearTexts();

        Ball.onBallHitGround += BallHitGroundCallback;
        Ball.onBallMissed += BallMissedCallback;
        Ball.onBallFellInWater += BallHitGroundCallback;
    }


    private void OnDestroy()
    {
        Ball.onBallHitGround -= BallHitGroundCallback;
        Ball.onBallMissed -= BallMissedCallback;
        Ball.onBallFellInWater -= BallHitGroundCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void BallHitGroundCallback(Vector3 ballHitPosition)
    {
        // 1. Calculate the score that we will add to the batsman
        float ballDistance = ballHitPosition.magnitude;

        int score = 2;

        if(ballDistance > firstRingRadius)
        {
            score += 2;
        }

        if (ballDistance > secondRingRadius)
        {
            score += 2;
        }

        // We know the value of the score at this point
        // This is the amount of the current Batsman scored
        onScoreCalculated?.Invoke(score);

        scoreTexts[currentOver].text = score.ToString();

        currentOver++;
    }


    private void BallMissedCallback()
    {
        scoreTexts[currentOver].text = "0";
        currentOver++;

        onScoreCalculated?.Invoke(0);
    }


    private void ClearTexts()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = "";
        }
    }
}
