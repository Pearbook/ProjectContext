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

    [Header("Timer")]
    [Tooltip("Time in minutes.")]
    public int TimeLimitMinutes;
    private float TimerLimit;

    private float timer;
    private float seconds;

    private bool gameHasEnded;

    [Header("Detection")]
    public Vector3 Offset;
    public Vector3 Size;
    public LayerMask Mask;

    private List<GameObject> AllObjects;

    public List<Range> AllRange;

    [Header("Bookcase")]
    public List<GameObject> AllBooks;

    private void Start()
    {
        TimerLimit = TimeLimitMinutes * 60;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CheckLevelForObjects();
        }

        if (timer >= TimerLimit)
        {
            // END THE GAME
            EndGame();
        }
        else
        {
            UpdateTimer();
        }
        
        if(AllRange.Count == 0)
            PlayerManager.Player.Controller.ObjectInRange = null;

        if (AllBooks.Count == 0)
            UserInterfaceManager.UI.ActivateCheckmark(4);
        else
            UserInterfaceManager.UI.DisableCheckmark(4);

    }

    public float GetCurrentGameplayTime()
    {
        return timer;
    }

    public void AddScore(int amount)
    {
        PlayerManager.Player.Stats.Score += amount;
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

    public void AddToRange(Range myRange)
    {
        if(!AllRange.Contains(myRange))
            AllRange.Add(myRange);
    }

    public void RemoveRange(Range myRange)
    {
        AllRange.Remove(myRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Offset, Size);
    }
}
