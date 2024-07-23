using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBatsman : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;
    [SerializeField] private Animator _animator;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;


    private const string IDLE = "Idle";
    private const string LEFT_WALKING = "Left";
    private const string RIGHT_WALKING = "Right";



    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = bowlerTarget.transform.position.x;

        // Calculate how far we are from the target
        float difference = targetPosition.x - transform.position.x;

        if (difference ==0)
        {
            // Play the Idle animation
            _animator.Play(IDLE);
        }

        else if (difference >0)
        {
            // Play the left strafe animation
            _animator.Play(LEFT_WALKING);
        }

        else
        {
            // Play the right strafe animation
            _animator.Play(RIGHT_WALKING);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
