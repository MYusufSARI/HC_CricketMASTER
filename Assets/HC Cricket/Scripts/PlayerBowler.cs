using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowler : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator animator;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;

    
    private void Start()
    {
        animator.Play("Run");
    }

    
    private void Update()
    {
        Run();
    }


    private void Run()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }
}
