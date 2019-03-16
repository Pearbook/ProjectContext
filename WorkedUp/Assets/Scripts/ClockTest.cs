using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTest : MonoBehaviour
{

    public Transform Clock;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;

    void Update()
    {
        if (seconds >= TimerLimit)
        {
            //seconds = 0;
            //timer = 0;
        }
        else
        {
            Clock.localEulerAngles = new Vector3(0, 0, -Custom.ReturnFillAmountRadial(seconds, TimerLimit));
            UpdateTimer();
        }
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }
}
