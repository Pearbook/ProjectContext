using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPractice : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform Destination;

    public float ExtraRotationSpeed;

    void Update()
    {
        Agent.destination = Destination.position;

        ExtraRotation();
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = Agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), ExtraRotationSpeed * Time.deltaTime);
    }

}
