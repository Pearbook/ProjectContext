using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Intensity = 1.0f;

    public bool isShaking = true;

    private Vector3 startingPos;

    private void Awake()
    {
        startingPos = transform.localPosition;
    }

    private void Update()
    {
        if(isShaking)
        {
            float step = Speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startingPos + Random.insideUnitSphere, step);
        }

        //transform.localPosition = startingPos + Random.insideUnitSphere * Amount;
        //transform.position = new Vector3(startingPos.x + Mathf.Sin(Time.time * Speed) * Amount, startingPos.y + Mathf.Sin(Time.time * Speed) * Amount, startingPos.z + Mathf.Sin(Time.time * Speed) * Amount);
    }
}
