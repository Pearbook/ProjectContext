using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [Header("Distance")]
    public float MyRange;
    private float dist;

    public bool isInRange;

    [Header("UI")]
    public CanvasGroup Instruction;

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

            if (Instruction != null)
                Instruction.alpha = 1;
        }
        else
        {
            GameplayManager.Gameplay.RemoveRange(this);

            if (Instruction != null)
                Instruction.alpha = 0;

            //PlayerManager.Player.Controller.ObjectInRange = null;
        }
    }
}
