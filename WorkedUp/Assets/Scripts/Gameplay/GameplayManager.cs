using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    static GameplayManager gameplay;

    public static GameplayManager Gameplay
    {
        get
        {
            if (gameplay == null)
            {
                gameplay = FindObjectOfType<GameplayManager>();
                if (gameplay == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    gameplay = obj.AddComponent<GameplayManager>();
                }
            }
            return gameplay;
        }
    }

    [Header("Detection")]
    public Vector3 Offset;
    public Vector3 Size;
    public LayerMask Mask;

    private List<GameObject> AllObjects;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CheckLevelForObjects();
        }
    }

    public void CheckLevelForObjects()
    {
        AllObjects = new List<GameObject>();

        foreach (Collider coll in CheckForObjectsOnFloor())
        {
            AllObjects.Add(coll.gameObject);
        }
    }

    Collider[] CheckForObjectsOnFloor()
    {
        Collider[] allColliders = Physics.OverlapBox(Offset, new Vector3(Size.x/2, Size.y/2, Size.z/2), Quaternion.identity, Mask);

        return allColliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Offset, Size);
    }
}
