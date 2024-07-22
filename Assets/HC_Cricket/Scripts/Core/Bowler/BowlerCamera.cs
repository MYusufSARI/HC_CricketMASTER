using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerCamera : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject aimingCamera;
    [SerializeField] private GameObject bowlingCamera;



    private void Awake()
    {
        BowlerManager.onAimingStarted += EnableAimingCamera;
        BowlerManager.onBowlingStarted += EnableBowlingCamera;
    }


    private void OnDestroy()
    {
        BowlerManager.onAimingStarted -= EnableAimingCamera;
        BowlerManager.onBowlingStarted -= EnableBowlingCamera;
    }


    private void EnableAimingCamera()
    {
        aimingCamera.SetActive(true);
        bowlingCamera.SetActive(false);
    }


    private void EnableBowlingCamera()
    {
        bowlingCamera.SetActive(true);
        aimingCamera.SetActive(false);
    }

}
