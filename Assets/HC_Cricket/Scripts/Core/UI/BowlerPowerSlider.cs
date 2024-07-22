using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlerPowerSlider : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Slider powerSlider;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;


    [Header(" Events ")]
    public static Action<float> onPowerSliderStopped;


    private bool canMove;



    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }


    public void StartMoving()
    {
        canMove = true;
    }


    public void StopMoving()
    {
        if (!canMove)
        {
            return;
        }

        // If we reach this point then canMove is true
        canMove = false;
        onPowerSliderStopped?.Invoke(powerSlider.value);
    }


    private void Move()
    {
        // (sin(f.x) +1) / 2
        powerSlider.value = (Mathf.Sin(Time.time * moveSpeed) + 1) / 2;
    }
}
