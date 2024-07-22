using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowler : MonoBehaviour
{
    public enum State { Idle, Aiming, Running, Bowling }

    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fakeBall;
    [SerializeField] private BallLauncher ballLauncher;
    [SerializeField] private GameObject ballTarget;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runDuration;
    [SerializeField] private float flightDurationMultiplier;
    private float runTimer;
    private float bowlingSpeed;

    private State state;



    private void Start()
    {
        state = State.Idle;
    }


    private void Update()
    {
        ManageState();
    }


    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Aiming:
                break;

            case State.Running:
                Run();
                break;

            case State.Bowling:
                break;
        }
    }


    public void StartRunning(float bowlingSpeed)
    {
        this.bowlingSpeed = bowlingSpeed;

        state = State.Running;
        animator.SetInteger("State", 1);
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
        state = State.Bowling;
        animator.SetInteger("State", 2);
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
    }
}
