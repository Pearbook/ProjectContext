using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{

    public List<GameObject> AllTrash;

    public PingPongScale Scale;

    private GameObject playerObj;

    [Header("Distance")]
    public float Range;
    private float dist;

    [Header ("UI")]
    public CanvasGroup InstructionGroup;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

        if (AllTrash.Count > 0)
        {
            if (dist < Range)
                InstructionGroup.alpha = 1;
            else
                InstructionGroup.alpha = 0;
        }
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            playerObj = PlayerManager.Player.Controller.holdObj;

            AddToTrashCan();
        }
        else
        {
            if (AllTrash.Count > 0)
                GetFromTrashCan();
        }
    }

    void AddToTrashCan()
    {
        playerObj.transform.parent = transform;
        playerObj.transform.position = transform.position;
        playerObj.SetActive(false);

        AllTrash.Add(playerObj);

        PlayerManager.Player.Controller.holdObj = null;

        if (Scale != null)
            Scale.PingPong();
    }

    void GetFromTrashCan()
    {
        AllTrash[AllTrash.Count - 1].SetActive(true);
        PlayerManager.Player.GiveHoldItem(AllTrash[AllTrash.Count - 1], false);

        AllTrash.RemoveAt(AllTrash.Count - 1);

        if (Scale != null)
            Scale.PingPong();
    }
}
