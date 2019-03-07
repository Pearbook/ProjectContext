using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public CookingPot CookingBehaviour;
    public WashingMachine WashingBehaviour;
    public Bookcase BookcaseBehaviour;
    public TrashCan TrashcanBehaviour;
    public DinnerTable DinnerTableBehaviour;
    public LaundryBasket BasketBehaviour;
    public MobilePhone MobileBehaviour;

    public void Interact()
    {
        if(CookingBehaviour != null)
            CookingBehaviour.Interaction();

        if (WashingBehaviour != null)
            WashingBehaviour.Interaction();

        if (BookcaseBehaviour != null)
            BookcaseBehaviour.Interaction();

        if (TrashcanBehaviour != null)
            TrashcanBehaviour.Interaction();

        if (DinnerTableBehaviour != null)
            DinnerTableBehaviour.Interaction();

        if (BasketBehaviour != null)
            BasketBehaviour.Interaction();

        if (MobileBehaviour != null)
            MobileBehaviour.Interaction();

    }

}
