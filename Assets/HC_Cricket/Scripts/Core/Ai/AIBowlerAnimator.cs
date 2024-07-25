using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBowlerAnimator : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private AIBowler aiBowler;


    private void ThrowBall()
    {
        aiBowler.ThrowBall();
    }
}
