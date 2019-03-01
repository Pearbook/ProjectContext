using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public CookingPot CookingBehaviour;
    public WashingMachine WashingBehaviour;

    public void Interact()
    {
        if(CookingBehaviour != null)
            CookingBehaviour.Interaction();

        if (WashingBehaviour != null)
            WashingBehaviour.Interaction();

    }

}
