using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryBasket : MonoBehaviour
{

    [Header ("Other")]
    public PingPongScale Scale;

    public void AddToBasket()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;

        PlayerManager.Player.RemoveHoldItem();

        if (Scale != null)
            Scale.PingPong();
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "laundry")
            {
                if(PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>() != null && PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>().isClean)
                    AddToBasket();
            }
        }
    }
}
