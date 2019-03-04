using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    [Header("Distance")]
    public float Range;
    private float dist;

    [Header("UI")]
    public CanvasGroup InstructionGroup;

    [Header("Properties")]
    public GameObject BookSlotContainer;
    public List<GameObject> AllBookSlots;
    private int slotIndex;

    public PingPongScale Scale;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "books")
            {
                if (dist < Range)
                    InstructionGroup.alpha = 1;
                else
                    InstructionGroup.alpha = 0;
            }
        }
    }

    public void AddToBookcase()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;
        PlayerManager.Player.Controller.holdObj = null;

        obj.GetComponent<BoxCollider>().enabled = false;
        obj.transform.parent = BookSlotContainer.transform;

        obj.transform.localEulerAngles = Vector3.zero;

        if (slotIndex < AllBookSlots.Count)
        {
            obj.transform.position = AllBookSlots[slotIndex].transform.position;
            slotIndex++;
        }

        InstructionGroup.alpha = 0;

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
