using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlerManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;
    [SerializeField] private BowlerPowerSlider powerSlider;
    [SerializeField] private PlayerBowler playerBowler;


    [SerializeField] private GameObject bowlingPanel;
    [SerializeField] private GameObject aimingPanel;


    [Header(" Elements ")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject drawPanel;
    [SerializeField] private CanvasGroup transitionCG;
    [SerializeField] private TextMeshProUGUI transitionScoreText;


    [Header(" Settings ")]
    [SerializeField] private Vector2 minMaxBowlingSpeed;
    [SerializeField] private AnimationCurve bowlingSpeedCurve;


    [Header(" Events ")]
    public static Action onAimingStarted;
    public static Action onBowlingStarted;
    public static Action onNextOverSet;


    private int currentOver;



    private void Start()
    {
        StartAiming();

        BowlerPowerSlider.onPowerSliderStopped += PowerSliderStoppedCallback;

        Ball.onBallMissed += BallMissedCallback;
        Ball.onBallHitGround += BallHitGroundCallback;
        Ball.onBallHitStump += BallHitStumpCallback;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        BowlerPowerSlider.onPowerSliderStopped -= PowerSliderStoppedCallback;

        Ball.onBallMissed -= BallMissedCallback;
        Ball.onBallHitGround -= BallHitGroundCallback;
        Ball.onBallHitStump -= BallHitStumpCallback;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }


    private void StartAiming()
    {
        // 1. Enable the movement of the BowlerTarget
        bowlerTarget.EnableMovement();

        // 2. Hide the Power Slider
        aimingPanel.SetActive(true);
        bowlingPanel.SetActive(false);

        // 3. Enable the Aiming Camera


        // 4. Disable the Bowling Camera
        onAimingStarted?.Invoke();
    }


    public void StartBowling()
    {
        // 1. Disable the movement of the Bowler Target
        bowlerTarget.DisableMovement();

        // 2. Show the Power Slider
        bowlingPanel.SetActive(true);
        aimingPanel.SetActive(false);

        onBowlingStarted?.Invoke();

        // 3. Enable the movement of the Power Slider
        powerSlider.StartMoving();
    }


    private void PowerSliderStoppedCallback(float power)
    {
        float lerp = bowlingSpeedCurve.Evaluate(power);
        float bowlingSpeed = Mathf.Lerp(minMaxBowlingSpeed.x, minMaxBowlingSpeed.y, lerp);

        playerBowler.StartRunning(bowlingSpeed);

        Debug.Log("Bowling Speed : " + bowlingSpeed);
    }


    private void BallHitGroundCallback(Vector3 ballHitPosition)
    {
        currentOver++;

        if (currentOver >= 3)
        {
            // We should either switch to the next game mode
            // Or we should end the game / Compare the scores

            if (GameManager.instance.TryStartingNextGameMode())
            {
                UpdateTransitionScore();

                ShowTransitionPanel();
            }
        }

        else
        {
            SetNextOver();
        }
    }


    private void UpdateTransitionScore()
    {
        transitionScoreText.text = "<color #00aaff>" + ScoreManager.instance.GetPlayerScore() +
                                   "</color> - <color #ff5500>" + ScoreManager.instance.GetAiScore() + "</color>";
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


    private void ShowTransitionPanel()
    {
        LeanTween.alphaCanvas(transitionCG, 1, 0.5f);
        transitionCG.interactable = true;
        transitionCG.blocksRaycasts = true;
    }


    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Win:
                ShowWinPanel();
                break;

            case GameState.Lose:
                ShowLosePanel();
                break;

            case GameState.Draw:
                ShowDrawPanel();
                break;
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


    public void NextButtonCallback()
    {
        GameManager.instance.NextButtonCallback();
    }
}
