using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongScale : MonoBehaviour
{
    public AnimationCurve Curve;
    public Vector3 Scale;
    public float Speed = 0.2f;

    public Transform ObjectToScale;

    private Vector3 finalScale;
    private float graphValue;
    

    private void Awake()
    {
        finalScale = Vector3.one;
        Curve.postWrapMode = WrapMode.PingPong;
    }

    public void PingPong()
    {
        StartCoroutine(BounceScale());
    }

    private IEnumerator BounceScale()
    {
        float i = 0;
        float rate = 1 / Speed;
        while (i < 1)
        {
            i += rate * Time.deltaTime;
            ObjectToScale.localScale = Vector3.Lerp(Scale, finalScale, Curve.Evaluate(i));
            yield return 0;
        }
    }

}
