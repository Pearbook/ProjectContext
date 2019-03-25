using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBed : MonoBehaviour
{

    public GameObject ChildContainer;
    private GameObject childObj;

    [Header ("Scale")]
    public PingPongScale Scale;

    public void PutInBed()
    {
        childObj.transform.parent = ChildContainer.transform;
        childObj.transform.localPosition = Vector3.zero;
        childObj.transform.localEulerAngles = new Vector3(0, 90, 0);

        childObj.GetComponent<NavMeshPractice>().DisableAgent();

        childObj.GetComponent<BabyBehaviour>().isInBed = true;
        childObj.GetComponent<BabyBehaviour>().bedTransform = ChildContainer.transform;

        PlayerManager.Player.Controller.holdObj = null;

        if (Scale != null)
            Scale.PingPong();
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "child")
            {
                childObj = PlayerManager.Player.Controller.holdObj;
                PutInBed();
            }
        }
    }
}
