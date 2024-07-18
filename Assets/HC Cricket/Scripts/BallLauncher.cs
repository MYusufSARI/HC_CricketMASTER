using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform from;
    [SerializeField] private Transform ballTarget;
    [SerializeField] private GameObject ballPrefab;


    [Header(" Settings ")]
    [SerializeField] private float flightDuration;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }


    public void LaunchBall()
    {
        // V0 = (p(t) - 1/2 gt*gy - p0) / t

        Vector3 pt = ballTarget.position;
        Vector3 gt2 = Physics.gravity * flightDuration * flightDuration / 2;
        Vector3 p0 = from.position;

        Vector3 initialVelocity = (pt - gt2 - p0) / flightDuration;

        GameObject ballInstance = Instantiate(ballPrefab, from.position, Quaternion.identity, transform);

        ballInstance.GetComponent<Rigidbody>().velocity = initialVelocity;
    }


    public void LaunchBall(Vector3 from, Vector3 to, float flightDuration)
    {
        Vector3 pt = to;
        Vector3 gt2 = Physics.gravity * flightDuration * flightDuration / 2;
        Vector3 p0 = from;

        Vector3 initialVelocity = (pt - gt2 - p0) / flightDuration;

        GameObject ballInstance = Instantiate(ballPrefab, from, Quaternion.identity, transform);

        ballInstance.GetComponent<Rigidbody>().velocity = initialVelocity;
    }
}
