using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBatsman : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private BowlerTarget bowlerTarget;


    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;



    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = bowlerTarget.transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
