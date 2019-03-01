using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Target;

    public float Damping;

    public bool xMoveOnly;

    public Vector2 Origin;
    public Vector2 ClampSize;

    public float ClampRadius;

    public void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, transform.position.y, Target.position.z), Damping);

        float x = Mathf.Clamp(transform.position.x, Origin.x -  ClampSize.x/2, Origin.x + ClampSize.x/2);

        float z = Mathf.Clamp(transform.position.z, Origin.y - ClampSize.y/2, Origin.y + ClampSize.y/2);

        if (!xMoveOnly)
            transform.position = new Vector3(x, transform.position.y, z);
        else
            transform.position = new Vector3(x, transform.position.y, Origin.y);

        /* // Circle Clamp
        Vector3 centerPosition = Vector3.zero;
        float distance = Vector3.Distance(transform.position, centerPosition);

        if(distance > ClampRadius)
        {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= ClampRadius / distance;
            transform.position = centerPosition + fromOriginToObject;
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(Origin.x, 0, Origin.y), new Vector3(ClampSize.x, 0, ClampSize.y));
    }
}
