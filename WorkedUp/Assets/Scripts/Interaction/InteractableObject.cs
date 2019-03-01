using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public CookingPot CookingBehaviour;
    public WashingMachine WashingBehaviour;
    public Bookcase BookcaseBehaviour;

    public void Interact()
    {
        if(CookingBehaviour != null)
            CookingBehaviour.Interaction();

        if (WashingBehaviour != null)
            WashingBehaviour.Interaction();

        if (BookcaseBehaviour != null)
            BookcaseBehaviour.Interaction();

    }

}
