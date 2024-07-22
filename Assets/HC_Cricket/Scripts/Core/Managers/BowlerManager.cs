using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;
    [SerializeField] private BowlerPowerSlider powerSlider;

    [SerializeField] private GameObject bowlingPanel;
    [SerializeField] private GameObject aimingPanel; 


    [Header(" Events ")]
    public static Action onAimingStarted;
    public static Action onBowlingStarted;



    private void Start()
    {
        StartAiming();
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
}
