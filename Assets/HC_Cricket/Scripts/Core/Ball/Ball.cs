using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header(" Settings ")]
    private bool hasHitGround;


    [Header(" Events ")]
    public static Action<Vector3> onBallHitGround;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        // Detect if the ball touches the field
        if (collision.collider.tag == "Field")
        {
            FieldCollidedCallback();
        }
    }


    private void FieldCollidedCallback()
    {
        if (hasHitGround)
        {
            return;
        }

        hasHitGround = true;

        onBallHitGround?.Invoke(transform.position);
    }
}
