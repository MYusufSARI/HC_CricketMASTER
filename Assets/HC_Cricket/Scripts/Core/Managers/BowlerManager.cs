using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;
    [SerializeField] private BowlerPowerSlider powerSlider;
    [SerializeField] private PlayerBowler playerBowler;


    [SerializeField] private GameObject bowlingPanel;
    [SerializeField] private GameObject aimingPanel;


    [Header(" Settings ")]
    [SerializeField] private Vector2 minMaxBowlingSpeed;
    [SerializeField] private AnimationCurve bowlingSpeedCurve;


    [Header(" Events ")]
    public static Action onAimingStarted;
    public static Action onBowlingStarted;



    private void Start()
    {
        StartAiming();

        BowlerPowerSlider.onPowerSliderStopped += PowerSliderStoppedCallback;
    }


    private void OnDestroy()
    {
        BowlerPowerSlider.onPowerSliderStopped -= PowerSliderStoppedCallback;
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
}
