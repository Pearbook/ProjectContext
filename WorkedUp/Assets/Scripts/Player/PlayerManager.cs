using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager player;

    public static PlayerManager Player
    {
        get
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerManager>();
                if (player == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    player = obj.AddComponent<PlayerManager>();
                }
            }
            return player;
        }
    }

    public GameObject PlayerObject;
    public PlayerController Controller;
    public PlayerStatus Stats;

    public void GiveHoldItem(GameObject item, bool isPrefab)
    {
        if (isPrefab)
        {
            GameObject prefab = (GameObject)Instantiate(item, Controller.ObjectPivot.transform.position, Quaternion.identity);

            prefab.transform.parent = Controller.ObjectPivot.transform;
            Controller.holdObj = prefab;
        }
        else
        {
            Controller.holdObj = item;

            if(item.tag == "child")
            {
                item.GetComponent<NavMeshAgent>().enabled = false;
                item.GetComponent<NavMeshPractice>().enabled = false;
            }

            if(Controller.holdObj.GetComponent<Rigidbody>() != null)
                Destroy(Controller.holdObj.GetComponent<Rigidbody>());

            Controller.holdObj.transform.parent = Controller.ObjectPivot.transform;
            Controller.holdObj.transform.localEulerAngles = new Vector3(0, 0, 0);
            Controller.holdObj.transform.position = Controller.ObjectPivot.transform.position;
        }
        
    }

    public void DropHoldItem(Vector2 ThrowForce, float ThrowSpin)
    {
        Rigidbody rigid = Controller.holdObj.AddComponent<Rigidbody>();

        if (Controller.holdObj.tag == "child")
        {
            Controller.holdObj.GetComponent<NavMeshAgent>().enabled = true;
            Controller.holdObj.GetComponent<NavMeshPractice>().enabled = true;

            rigid.useGravity = false;
            rigid.isKinematic = true;
        }

        rigid.AddForce(PlayerObject.transform.TransformDirection(new Vector3(0, 1 * ThrowForce.y, 1 * ThrowForce.x)));
        rigid.angularVelocity = new Vector3(ThrowSpin, ThrowSpin, ThrowSpin);

        Controller.holdObj = null;
    }

    public void RemoveHoldItem()
    {
        Destroy(Controller.holdObj);
        Controller.holdObj = null;
    }

    public void DisablePlayer()
    {
        Controller.allowMovement = false;
        Controller.isDisabled = true;

        Controller.PlayerRigid.velocity = Vector3.zero;
    }

}
