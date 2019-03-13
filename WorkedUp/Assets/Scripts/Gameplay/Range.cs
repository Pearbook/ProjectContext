using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [Header("Distance")]
    public float MyRange;
    private float dist;

    public bool isInRange;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

        if (dist < MyRange)
            isInRange = true;
        else
            isInRange = false;

        if(isInRange)
        {
            GameplayManager.Gameplay.AddToRange(this);

            if(GetComponent<InteractableObject>() != null)
                PlayerManager.Player.Controller.ObjectInRange = GetComponent<InteractableObject>();
        }
        else
        {
            GameplayManager.Gameplay.RemoveRange(this);
            //PlayerManager.Player.Controller.ObjectInRange = null;
        }
    }
}
