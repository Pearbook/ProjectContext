using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(Controller.holdObj.GetComponent<Rigidbody>());

            Controller.holdObj.transform.parent = Controller.ObjectPivot.transform;
            Controller.holdObj.transform.localEulerAngles = new Vector3(0, 0, 0);
            Controller.holdObj.transform.position = Controller.ObjectPivot.transform.position;
        }
        
    }

    public void RemoveHoldItem()
    {
        Destroy(Controller.holdObj);
        Controller.holdObj = null;
    }

}
