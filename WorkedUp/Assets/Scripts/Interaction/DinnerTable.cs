using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerTable : MonoBehaviour
{
    // Check of je de kookpot vast heb
    // Interact
    // Enable hidden borden and so
    // Un-parent kookpot van speler.
    // Parent kookpot naar tafel
    // plaats de kookpot op aangewezen plaats.

    public GameObject SetTableContainer;
    public GameObject CookPotContainer;

    [Header("Particle Effect")]
    public GameObject SmokeEffect;

    [Header("Score")]
    public int ScoreWhenDone;

    [Header ("Other")]
    public PingPongScale Scale;

    private bool isSet;

    void SetTable()
    {
        SetTableContainer.SetActive(true);

        GameObject obj = PlayerManager.Player.Controller.holdObj;
        PlayerManager.Player.Controller.holdObj = null;

        obj.GetComponent<BoxCollider>().enabled = false;
        obj.transform.parent = CookPotContainer.transform;

        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;

        if(obj.GetComponent<PickUpProperties>() != null)
        {
            if(!obj.GetComponent<PickUpProperties>().isBurned)
            {
                // ERROR INDICATOR
                UserInterfaceManager.UI.SpawnErrorIndicator(transform, true);

                // ADD SCORE
                GameplayManager.Gameplay.AddScore(ScoreWhenDone);
            }
            else
            {
                // ERROR INDICATOR
                UserInterfaceManager.UI.SpawnErrorIndicator(transform, false);
            }
        }

        if (Scale != null)
            Scale.PingPong();

        isSet = true;

        UserInterfaceManager.UI.ActivateCheckmark(1);

        SmokeEffect.SetActive(true);
    }

    public void Interaction()
    {
        if (!isSet)
        {
            if (PlayerManager.Player.Controller.holdObj != null)
            {
                if (PlayerManager.Player.Controller.holdObj.tag == "food")
                    SetTable();
            }
        }
    }
}
