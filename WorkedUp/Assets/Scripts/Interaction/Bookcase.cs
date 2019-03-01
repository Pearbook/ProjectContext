using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    // Interact while holding a book
    // Unparent hold book
    // Parent book to bookcase book container
    // Reset rotation
    // Move book to a random set position (AllBookSlots)

    [Header("Properties")]
    public GameObject BookSlotContainer;
    public List<GameObject> AllBookSlots;

    public PingPongScale Scale;

    public void AddToBookcase()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;
        PlayerManager.Player.Controller.holdObj = null;

        obj.GetComponent<BoxCollider>().enabled = false;
        obj.transform.parent = BookSlotContainer.transform;

        obj.transform.localEulerAngles = Vector3.zero;

        if (Scale != null)
            Scale.PingPong();
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "books")
            {
                AddToBookcase();
            }
        }
    }
}
