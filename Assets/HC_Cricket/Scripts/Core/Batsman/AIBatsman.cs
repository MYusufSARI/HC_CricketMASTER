using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIBatsman : MonoBehaviour
{
    private enum State { Moving, Hitting }

    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider batsCollider;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private Vector2 mixMaxHitVelocity;
    [SerializeField] private float maxHitDuration;


    [Header(" Events ")]
    public static Action<Transform> onBallHit;


    private State _state;
    private bool canDetectHits;
    private float hitTimer;


    private const string IDLE = "Idle";
    private const string LEFT_WALKING = "Left";
    private const string RIGHT_WALKING = "Right";
    private const string HIT = "Hit";



    private void Start()
    {
        _state = State.Moving;

        PlayerBowler.onBallThrown += BallThrownCallback;
    }


    private void OnDestroy()
    {
        PlayerBowler.onBallThrown -= BallThrownCallback;
    }


    private void Update()
    {
        ManageState();
    }


    private void ManageState()
    {
        switch (_state)
        {
            case State.Moving:
                Move();
                break;

            case State.Hitting:
                if (canDetectHits)
                    CheckForHits();
                break;
        }
    }


    private void Move()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = GetTargetX();

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


    private void BallThrownCallback(float ballFlightDuration)
    {
        _state = State.Hitting;

        StartCoroutine(WaitAndHitCoroutine());

        IEnumerator WaitAndHitCoroutine()
        {
            float bestDelay = ballFlightDuration - 0.5f;

            yield return new WaitForSeconds(bestDelay);

            _animator.Play(HIT);
        }
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

        ball.GetComponent<Rigidbody>().velocity = (Vector3.back + Vector3.up + Vector3.right * Random.Range(-1f, 1f )) * hitVelocity;

        onBallHit?.Invoke(ball);
    }


    private float GetTargetX()
    {
        Vector3 bowlerShootPosition = new Vector3(-1, 0, -9.5f);
        Vector3 shootDirection = (bowlerTarget.transform.position - bowlerShootPosition).normalized;

        float shootAngle = Vector3.Angle(Vector3.right, shootDirection);

        // bc represents the distance betwwen AIBatsman position on the Z axis and the position
        // // where the bowler will shoot from

        float bc = transform.position.z - bowlerShootPosition.z;
        float ab = bc / Mathf.Sin(shootAngle * Mathf.Deg2Rad);

        Vector3 targetAiPosition = bowlerShootPosition + shootDirection.normalized * ab;

        return targetAiPosition.x + 0.5f;
    }
}
