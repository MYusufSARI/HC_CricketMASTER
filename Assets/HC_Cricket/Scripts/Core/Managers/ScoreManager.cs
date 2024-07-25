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
        LocalScoreManager.onScoreCalculated += ScoreCalculatedCallBack;
    }


    private void OnDestroy()
    {
        LocalScoreManager.onScoreCalculated -= ScoreCalculatedCallBack;
    }


    private void ScoreCalculatedCallBack(int score)
    {

    }
}
