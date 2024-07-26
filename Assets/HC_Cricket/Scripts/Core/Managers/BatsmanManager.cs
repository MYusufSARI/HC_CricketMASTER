using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatsmanManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject drawPanel;
    [SerializeField] private CanvasGroup transitionCG;
    [SerializeField] private TextMeshProUGUI transitionScoreText;


    [Header(" EndGame Score Texts ")]
    [SerializeField] private TextMeshProUGUI[] endScoreTexts;


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

        winPanel.SetActive(false);
        drawPanel.SetActive(false);
        losePanel.SetActive(false);

        StartAiming();

        Ball.onBallMissed += BallMissedCallback;
        Ball.onBallHitGround += BallHitGroundCallback;
        Ball.onBallHitStump += BallHitStumpCallback;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        Ball.onBallMissed -= BallMissedCallback;
        Ball.onBallHitGround -= BallHitGroundCallback;
        Ball.onBallHitStump -= BallHitStumpCallback;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
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

            if (GameManager.instance.TryStartingNextGameMode())
            {
                UpdateTransitionScore();

                ShowTransitionPanel();
            }

            else
            {
                // This is when the GameManager returns false
                // This means that we ended the game
                UpdateEndGameScoreTexts();
            }
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


    private void ShowTransitionPanel()
    {
        LeanTween.alphaCanvas(transitionCG, 1, 0.5f);
        transitionCG.interactable = true;
        transitionCG.blocksRaycasts = true;
    }


    private void UpdateTransitionScore()
    {
        transitionScoreText.text = "<color #00aaff>" + ScoreManager.instance.GetPlayerScore() +
                                   "</color> - <color #ff5500>" + ScoreManager.instance.GetAiScore() + "</color>";
    }


    private void UpdateEndGameScoreTexts()
    {
        for (int i = 0; i < endScoreTexts.Length; i++)
        {
            endScoreTexts[i].text = transitionScoreText.text = "<color #00aaff>" + ScoreManager.instance.GetPlayerScore() +
                                   "</color> - <color #ff5500>" + ScoreManager.instance.GetAiScore() + "</color>";
        }
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
