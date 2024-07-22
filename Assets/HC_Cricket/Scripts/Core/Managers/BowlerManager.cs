using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;


    private void StartAiming()
    {
        // 1. Enable the movement of the BowlerTarget
        bowlerTarget.EnableMovement();

        // 2. Hide the Power Slider


        // 3. Enable the Aiming Camera


        // 4. Disable the Bowling Camera

    }


    private void StartBowling()
    {

    }
}
