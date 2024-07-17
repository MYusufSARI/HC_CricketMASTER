using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowler : MonoBehaviour
{
    public enum State { Idle, Aiming, Running, Bowling }

    [Header(" Elements ")]
    [SerializeField] private Animator animator;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runDuration;
    private float runTimer;

    private State state;



    private void Start()
    {
        StartRunning();
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


    private void StartRunning()
    {
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
}
