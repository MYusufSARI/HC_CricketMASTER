using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBatsman : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider batsCollider;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private Vector2 mixMaxHitVelocity;
    [SerializeField] private float maxHitDuration;


    [Header(" Events ")]
    public static Action<Transform> onBallHit;


    private bool canDetectHits;
    private float hitTimer;


    private const string IDLE = "Idle";
    private const string LEFT_WALKING = "Left";
    private const string RIGHT_WALKING = "Right";
    private const string HIT = "Hit";



    private void Start()
    {
        BowlerManager.onNextOverSet += Restart;
    }


    private void OnDestroy()
    {
        BowlerManager.onNextOverSet -= Restart;
    }


    private void Update()
    {
        if (canDetectHits)
            CheckForHits();
    }


    private void Move()
    {
        Vector3 targetPosition = transform.position;
        //targetPosition.x = GetTargetX();

        targetPosition.x = Mathf.Clamp(targetPosition.x, -1.83f, 1.83f);

        // Calculate how far we are from the target
        float difference = targetPosition.x - transform.position.x;

        if (difference == 0)
        {
            // Play the Idle animation
            _animator.Play(IDLE);
        }

        else if (difference > 0)
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


    public void StartDetectingHits()
    {
        canDetectHits = true;
        hitTimer = 0;
    }


    private void CheckForHits()
    {
        Vector3 center = batsCollider.transform.TransformPoint(batsCollider.center);
        Vector3 halfExtents = 1.5f * batsCollider.size / 2;

        Quaternion rotation = batsCollider.transform.rotation;

        Collider[] detectedBalls = Physics.OverlapBox(center, halfExtents, rotation, ballMask);

        for (int i = 0; i < detectedBalls.Length; i++)
        {
            BallDetectedCallback(detectedBalls[i]);
            return;
        }

        hitTimer += Time.deltaTime;
    }


    private void BallDetectedCallback(Collider ballCollider)
    {
        canDetectHits = false;

        ShootBall(ballCollider.transform);
    }


    private void ShootBall(Transform ball)
    {
        // Compare the Hit Timer with the Max Duration
        // if hitTimer =0 => maxHitVelocity
        // if hitTimer > maxHitDuration => minVelocity

        float lerp = Mathf.Clamp01(hitTimer / maxHitDuration);
        float hitVelocity = Mathf.Lerp(mixMaxHitVelocity.y, mixMaxHitVelocity.x, lerp);

        Vector3 hitVelocityVector = (Vector3.back + Vector3.up + Vector3.right * Random.Range(-1f, 1f)) * hitVelocity;

        ball.GetComponent<Ball>().GetHitByBat(hitVelocityVector);

        onBallHit?.Invoke(ball);
    }


    private void Restart()
    {
        
    }
}
