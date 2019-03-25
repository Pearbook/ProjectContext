using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool isHalfway;
    private bool gameHasEnded;

    [Header("Detection")]
    public Vector3 Offset;
    public Vector3 Size;
    public LayerMask Mask;

    private List<GameObject> AllObjects;

    public List<Range> AllRange;

    [Header("UI")]
    public CanvasGroup TimeIndicator;
    public Text IndicatorText;
    public Text IndicatorTextShade;

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
            seconds = 50;
            timer = 50;
        }

        if (timer >= TimerLimit)
        {
            // END THE GAME
            EndGame();
        }
        else
        {
            UpdateTimer();

            if (!isHalfway)
            {
                if (timer >= TimerLimit / 2)
                {
                    isHalfway = true;
                    StartCoroutine(IndicatorDisplayDelay());
                }
            }

            if (timer >= TimerLimit - 5)
            {
                TimeIndicator.alpha = 1;

                IndicatorText.text = "5";
                IndicatorTextShade.text = "5";
            }

            if (timer >= TimerLimit - 4)
            {
                IndicatorText.text = "4";
                IndicatorTextShade.text = "4";
            }

            if (timer >= TimerLimit - 3)
            {
                IndicatorText.text = "3";
                IndicatorTextShade.text = "3";
            }

            if (timer >= TimerLimit - 2)
            {
                IndicatorText.text = "2";
                IndicatorTextShade.text = "2";
            }

            if (timer >= TimerLimit - 1)
            {
                IndicatorText.text = "1";
                IndicatorTextShade.text = "1";
            }

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

        if(PlayerManager.Player.Stats.Score < 0)
                PlayerManager.Player.Stats.Score = 0;
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

    IEnumerator IndicatorDisplayDelay()
    {
        TimeIndicator.alpha = 1;
        yield return new WaitForSeconds(3.0f);
        TimeIndicator.alpha = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Offset, Size);
    }
}
