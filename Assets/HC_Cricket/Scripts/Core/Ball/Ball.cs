using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header(" Settings ")]
    private bool hasHitGround;
    private bool hasBeenHitByBat;


    [Header(" Events ")]
    public static Action<Vector3> onBallHitGround;
    public static Action onBallMissed;



    private void OnCollisionEnter(Collision collision)
    {
        // Detect if the ball touches the field
        if (collision.collider.tag == "Field")
        {
            FieldCollidedCallback();
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag =="Water")
        {
            WaterTriggeredCallback();
        }

        else if (collider.tag =="Miss")
        {
            MissTriggeredCallback();
        }
    }


    private void FieldCollidedCallback()
    {
        if (!hasBeenHitByBat)
        {
            return;
        }

        if (hasHitGround)
        {
            return;
        }

        hasHitGround = true;

        onBallHitGround?.Invoke(transform.position);
    }


    private void WaterTriggeredCallback()
    {
        if (!hasBeenHitByBat)
        {
            return;
        }

        if (hasHitGround)
        {
            return;
        }
        hasHitGround = true;

        onBallHitGround?.Invoke(transform.position);
    }


    private void MissTriggeredCallback()
    {
        if (hasBeenHitByBat)
        {
            return;
        }

        if (hasHitGround)
        {
            return;
        }
        hasHitGround = true;

        onBallMissed?.Invoke();
    }


    public void GetHitByBat(Vector3 velocity)
    {
        hasBeenHitByBat = true;
        GetComponent<Rigidbody>().velocity = velocity;
    }
}
