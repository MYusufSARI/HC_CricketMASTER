using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatsmanAnimator : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private PlayerBatsman playerBatsman;



    public void StartDetectingHits()
    {
        playerBatsman.StartDetectingHits();
    }


    public void StopDetectingHits()
    {
        playerBatsman.StopDetectingHits();
    }
}
