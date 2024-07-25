using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIBowler : MonoBehaviour
{
    public enum State { Idle, Aiming, Running, Bowling }

    [Header(" Elements ")]
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject fakeBall;
    [SerializeField] private BallLauncher ballLauncher;
    [SerializeField] private GameObject ballTarget;
    [SerializeField] private BowlerTarget bowlerTarget;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runDuration;
    [SerializeField] private float flightDurationMultiplier;
    private float runTimer;
    private float bowlingSpeed;
    private Vector3 initialPosition;


    [Header(" Events ")]
    public static Action<float> onBallThrown;

    private const string IDLE = "Idle";

    private State _state;



    private void Start()
    {
        _state = State.Aiming;

        initialPosition = transform.position;

        BowlerManager.onNextOverSet += Restart;
    }


    private void OnDestroy()
    {
        BowlerManager.onNextOverSet -= Restart;
    }


    private void Update()
    {
        ManageState();
    }


    private void ManageState()
    {
        switch (_state)
        {
            case State.Idle:
                break;

            case State.Aiming:
                Aim();
                break;

            case State.Running:
                Run();
                break;

            case State.Bowling:
                break;
        }
    }


    private void Aim()
    {
        float x = Mathf.Sin(Time.time);
        float y = Mathf.Sin(Time.time * 2);

        Vector2 targetPosition = new Vector2(x, y);

        bowlerTarget.Move(targetPosition);
    }


    public void StartRunning(float bowlingSpeed)
    {
        this.bowlingSpeed = bowlingSpeed;

        runTimer = 0;

        _state = State.Running;
        _animator.SetInteger("State", 1);
    }


    private void Run()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;

        runTimer += Time.deltaTime;

        if (runTimer >= runDuration)
        {
            StartBowling();
        }
    }


    private void StartBowling()
    {
        _state = State.Bowling;
        _animator.SetInteger("State", 2);
    }


    public void ThrowBall()
    {
        fakeBall.SetActive(false);

        Vector3 from = fakeBall.transform.position;
        Vector3 to = ballTarget.transform.position;

        // Calculate the duration of the flight depending on the bowlingSpeed
        // velocity = distance / time

        float distance = Vector3.Distance(from, to);

        // 1 meter equals 3.6 km/h
        float velocity = bowlingSpeed / 3.6f;

        float duration = flightDurationMultiplier * distance / velocity;

        print("Duration : " + duration);

        ballLauncher.LaunchBall(from, to, duration);

        onBallThrown?.Invoke(duration);
    }


    private void Restart()
    {
        _state = State.Idle;
        transform.position = initialPosition;

        _animator.SetInteger("State", 0);
        _animator.Play(IDLE);

        fakeBall.SetActive(true);
    }
}
