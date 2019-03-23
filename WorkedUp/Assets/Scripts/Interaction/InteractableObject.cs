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
    public Microwave MicrowaveBehaviour;
    public BabyBed BabyBedBehaviour;
    public Couch CouchBehaviour;

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

        if (MicrowaveBehaviour != null)
            MicrowaveBehaviour.Interaction();

        if (BabyBedBehaviour != null)
            BabyBedBehaviour.Interaction();

        if (CouchBehaviour != null)
            CouchBehaviour.Interaction();

    }

    public void MakeMess()
    {
        if (BookcaseBehaviour != null)
            BookcaseBehaviour.EmptyBookcase();

        if (TrashcanBehaviour != null)
            TrashcanBehaviour.EmptyTrashCan();

        if (BasketBehaviour != null)
            BasketBehaviour.EmptyBasket();
    }
}
