using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBehaviour : MonoBehaviour
{
    // Baby starts in bed
    // Timer starts running.
    // Baby gets bored (timer ended)
    // Baby gets out of bed.
    // Start NavMesh.

    public bool isInBed;
    public Transform bedTransform;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;

    private void Update()
    {
        if(isInBed)
        {
            if (seconds >= TimerLimit)
            {
                // Get out bed
                GetOut();
            }
            else
            {
                UpdateTimer();
            }
        }
    }

    void GetOut()
    {
        transform.parent = null;
        //transform.position = new Vector3(bedTransform.position.x - 1.5f, bedTransform.position.y, bedTransform.position.y);

        isInBed = false;

        seconds = 0;
        timer = 0;

        this.gameObject.GetComponent<NavMeshPractice>().enabled = true;
        this.gameObject.GetComponent<NavMeshPractice>().EnableAgent();

        //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }
}
