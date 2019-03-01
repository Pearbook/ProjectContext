using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{

    public Vector3 Size;
    public Vector3 Offset;

    public PingPongScale Scale;

    public LayerMask Mask;

    void FixedUpdate()
    {
        if(CheckForObject() != null)
        {
            Destroy(CheckForObject().gameObject);
            Scale.PingPong();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Offset, Size);
    }

    Collider CheckForObject()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + Offset, Size/2, Quaternion.identity, Mask);

        if (hitColliders.Length > 0)
            return hitColliders[0];
        else
            return null;
    }
}
