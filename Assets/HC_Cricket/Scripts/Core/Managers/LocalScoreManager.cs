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


    private void Start()
    {
        ClearTexts();

        Ball.onBallHitGround += BallHitGroundCallback;
    }


    private void OnDestroy()
    {
        Ball.onBallHitGround -= BallHitGroundCallback;
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

        scoreTexts[0].text = score.ToString();
    }


    private void ClearTexts()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = "";
        }
    }
}
