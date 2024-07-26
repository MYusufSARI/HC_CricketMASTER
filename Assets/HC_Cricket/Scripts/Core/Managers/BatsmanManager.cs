using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsmanManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject drawPanel;


    [Header(" Settings ")]
    [SerializeField] private Vector2 minMaxBowlingSpeed;
    [SerializeField] private AnimationCurve bowlingSpeedCurve;


    [Header(" Events ")]
    public static Action onAimingStarted;
    public static Action onBowlingStarted;
    public static Action onNextOverSet;


    private int currentOver;



    IEnumerator Start()
    {
        yield return null;

        StartAiming();

        Ball.onBallMissed += BallMissedCallback;
        Ball.onBallHitGround += BallHitGroundCallback;
        Ball.onBallHitStump += BallHitStumpCallback;
    }


    private void OnDestroy()
    {
        Ball.onBallMissed -= BallMissedCallback;
        Ball.onBallHitGround -= BallHitGroundCallback;
        Ball.onBallHitStump -= BallHitStumpCallback;
    }


    private void StartAiming()
    {
        onAimingStarted?.Invoke();
    }


    public void StartBowling()
    {
        onBowlingStarted?.Invoke();
    }


    private void BallHitGroundCallback(Vector3 ballHitPosition)
    {
        currentOver++;

        if (currentOver >= 3)
        {
            // We should either switch to the next game mode
            // Or we should end the game / Compare the scores

            GameManager.instance.TryStartingNextGameMode();
        }

        else
        {
            SetNextOver();
        }
    }


    private void BallMissedCallback()
    {
        BallHitGroundCallback(Vector3.zero);
    }


    private void BallHitStumpCallback()
    {
        currentOver = 2;

        BallHitGroundCallback(Vector3.zero);
    }


    private void SetNextOver()
    {
        StartCoroutine(WaitAndRestart());

        IEnumerator WaitAndRestart()
        {
            yield return new WaitForSeconds(2);

            onNextOverSet?.Invoke();

            StartAiming();
        }
    }


    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }


    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }


    public void ShowDrawPanel()
    {
        drawPanel.SetActive(true);
    }
}
