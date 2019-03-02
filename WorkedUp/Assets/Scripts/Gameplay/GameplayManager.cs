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

    [Header ("Timer")]
    [Tooltip("Time in seconds.")]
    public float TimerLimit;

    private float timer;
    private float seconds;

    private bool gameHasEnded;

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

        if (seconds >= TimerLimit)
        {
            // END THE GAME
            EndGame();
        }
        else
        {
            UpdateTimer();
        }
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    public void EndGame()
    {
        if(!gameHasEnded)
        {
            CheckLevelForObjects();

            PlayerManager.Player.DisablePlayer();
            UserInterfaceManager.UI.DisplayScoreScreen();

            gameHasEnded = true;
        }
    }

    public void CheckLevelForObjects()
    {
        AllObjects = new List<GameObject>();

        foreach (Collider coll in CheckForObjectsOnFloor())
        {
            AllObjects.Add(coll.gameObject);
        }

        PlayerManager.Player.Stats.Score -= AllObjects.Count;
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
