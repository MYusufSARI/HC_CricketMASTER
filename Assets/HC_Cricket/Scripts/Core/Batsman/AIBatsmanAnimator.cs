using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBatsmanAnimator : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private AIBatsman aiBatsman;



    public void StartDetectingHit()
    {
        aiBatsman.StartDetectingHits();
    }
}
