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

    [Header("Particle Effect")]
    public ParticleSystem SmokePuff;

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

        if(playerObj.GetComponent<PickUpProperties>() != null)
            GameplayManager.Gameplay.AddScore(playerObj.GetComponent<PickUpProperties>().PointsOnTrash);

        AllTrash.Add(playerObj);

        PlayerManager.Player.Controller.holdObj = null;

        if(!SmokePuff.isPlaying)
            SmokePuff.Play();

        if (Scale != null)
            Scale.PingPong();
    }

    void GetFromTrashCan()
    {
        AllTrash[AllTrash.Count - 1].SetActive(true);
        PlayerManager.Player.GiveHoldItem(AllTrash[AllTrash.Count - 1], false);

        AllTrash.RemoveAt(AllTrash.Count - 1);

        InstructionGroup.alpha = 0;

        if (Scale != null)
            Scale.PingPong();
    }

    public void EmptyTrashCan()
    {
        for(int i = 0; i < AllTrash.Count; ++i)
        {
            AllTrash[i].SetActive(true);

            AllTrash[i].GetComponent<Collider>().isTrigger = false;
            AllTrash[i].AddComponent<Rigidbody>();

            AllTrash[i].transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 0.5f);
            AllTrash[i].transform.parent = null;

            if (AllTrash[i].GetComponent<PickUpProperties>() != null)
                GameplayManager.Gameplay.AddScore(-AllTrash[i].GetComponent<PickUpProperties>().PointsOnTrash);

            AllTrash.RemoveAt(i);
        }
        
        if (Scale != null)
            Scale.PingPong();
    }
}
