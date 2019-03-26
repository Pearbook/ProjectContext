using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPractice : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform Destination;

    public List<Transform> AllDestinations;
    private int destinationIndex;

    public float ExtraRotationSpeed;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;
    private bool isMakingMess;

    [Header("Other")]
    public GameObject DropShadow;
    private bool isDisabled;

    void Update()
    {
        if (!GameplayManager.Gameplay.isDisabled)
        {
            if (!isDisabled)
            {
                Agent.destination = new Vector3(AllDestinations[destinationIndex].position.x, AllDestinations[destinationIndex].position.y, AllDestinations[destinationIndex].position.z - 1);


                if (Vector3.Distance(transform.position, AllDestinations[destinationIndex].position) <= 2)
                {
                    if (!isMakingMess)
                    {
                        if (seconds >= TimerLimit / 2)
                        {
                            isMakingMess = true;
                            AllDestinations[destinationIndex].GetComponent<InteractableObject>().MakeMess();
                        }
                    }

                    if (seconds >= TimerLimit)
                    {
                        seconds = 0;
                        timer = 0;

                        isMakingMess = false;

                        destinationIndex = Random.Range(0, AllDestinations.Count);

                    }
                    else
                    {
                        UpdateTimer();
                    }

                }

                ExtraRotation();
            }
        }
    }

    public void DisableAgent()
    {
        Agent.enabled = false;
        isDisabled = true;

        DropShadow.SetActive(false);
    }

    public void EnableAgent()
    {
        Agent.enabled = true;
        isDisabled = false;

        DropShadow.SetActive(true);
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = Agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), ExtraRotationSpeed * Time.deltaTime);
    }

}
