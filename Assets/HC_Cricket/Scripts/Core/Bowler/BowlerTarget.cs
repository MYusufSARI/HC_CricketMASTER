using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlerTarget : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private Vector2 minMaxZ;
    [SerializeField] private Vector2 moveSpeed;


    private Vector3 clickedPosition;
    private Vector3 clickedTargetPosition;

    private bool canMove;



    private void Update()
    {
        if (canMove)
        {
            ManageControl();
        }
    }


    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedPosition = Input.mousePosition;
            clickedTargetPosition = transform.position;
        }

        else if (Input.GetMouseButton(0))
        {
            Vector3 difference = Input.mousePosition - clickedPosition;

            difference.x /= Screen.width;
            difference.y /= Screen.height;

            Vector3 targetPosition = clickedTargetPosition + new Vector3(difference.x * moveSpeed.x, 0, difference.y * moveSpeed.y);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minMaxX.x, minMaxX.y);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minMaxZ.x, minMaxZ.y);

            transform.position = targetPosition;
        }
    }


    public void Move(Vector2 targetPosition)
    {
        transform.position = new Vector3(targetPosition.x, 0, targetPosition.y);
    }


    public void EnableMovement()
    {
        canMove = true;
    }


    public void DisableMovement()
    {
        canMove = false;
    }
}
