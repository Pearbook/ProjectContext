using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashingMachine : MonoBehaviour
{

    // Interact while holding laundry
    // Put laundry in the machine, Remove object from player hands!
    // Add to count
    // Wait for count to reach limit
    // Start machine (Timer)
    // When finished, display checkmark and wait for player interactin
    // Give player result object

    [Header ("Properties")]
    public int ItemLimit;
    private int currentItemCount;
    public GameObject PrefabToSpawn;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;
    private bool waitForTimer;
    private bool isTimerDone;

    [Header("Distance")]
    public float Range;
    private float dist;

    [Header("UI")]
    public CanvasGroup InstructionGroup;
    public CanvasGroup BarGroup;
    public Image ProgressBar;
    public CanvasGroup Checkmark;
    public CanvasGroup Warning;

    public GameObject SpotContainer;
    public List<GameObject> AllSpots;

    [Header("Score")]
    public int ScorePerClothing;

    [Header("Other")]
    public PingPongScale Scale;

    public void Start()
    {
        SpawnUI();
    }

    public void Update()
    {
        if (!GameplayManager.Gameplay.isDisabled)
        {
            dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

            if (PlayerManager.Player.Controller.holdObj != null)
            {
                if (PlayerManager.Player.Controller.holdObj.tag == "laundry")
                {
                    if (dist < Range)
                        InstructionGroup.alpha = 1;
                    else
                        InstructionGroup.alpha = 0;
                }
            }

            if (currentItemCount == ItemLimit)
            {
                if (!isTimerDone)
                {
                    BarGroup.alpha = 1;

                    if (seconds >= TimerLimit)
                    {
                        isTimerDone = true;

                        UserInterfaceManager.UI.ActivateCheckmark(2);

                        seconds = 0;
                        timer = 0;
                    }
                    else
                    {
                        UpdateTimer();

                        ProgressBar.fillAmount = Custom.ReturnFillAmount(seconds, TimerLimit);
                        SpotContainer.SetActive(false);
                    }
                }
                else
                {
                    BarGroup.alpha = 0;

                    if (Checkmark != null)
                        Checkmark.alpha = 1;
                }
            }

            if (currentItemCount == 0)
            {
                Checkmark.alpha = 0;
            }

            // SPOT UI
            if (AllSpots.Count > 0)
            {
                if (currentItemCount > 0)
                {
                    for (int i = 0; i < currentItemCount; ++i)
                    {
                        AllSpots[i].GetComponent<Image>().color = new Color32(90, 188, 70, 255);
                    }
                }
            }
        }
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    public void AddToMachine()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;

        PlayerManager.Player.RemoveHoldItem();
        currentItemCount++;

        InstructionGroup.alpha = 0;

        // ERROR INDICATOR
        UserInterfaceManager.UI.SpawnErrorIndicator(transform, true);

        // ADD SCORE
        GameplayManager.Gameplay.AddScore(ScorePerClothing);

        if (Scale != null)
            Scale.PingPong();
    }

    public void RemoveFromMachine()
    {
        if (Scale != null)
            Scale.PingPong();

        currentItemCount--;

        PlayerManager.Player.GiveHoldItem(PrefabToSpawn, true);
    }

    public void Interaction()
    {
        if(PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "laundry")
            {
                if (PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>() != null)
                {
                    if (!PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>().isClean)
                        if (currentItemCount < ItemLimit)
                            AddToMachine();
                }
            }
        }
        else
        {
            if (isTimerDone)
            {
                if(currentItemCount > 0)
                    RemoveFromMachine();
            }
        }
    }

    public void SpawnUI()
    {
        for(int i = 0; i < ItemLimit; ++i)
        {
            Vector3 newPos = new Vector3(SpotContainer.transform.position.x + 0.2f * i, SpotContainer.transform.position.y, SpotContainer.transform.position.z);

            GameObject spot = (GameObject)Instantiate(Resources.Load("UI/ui_countSpot"), newPos, Quaternion.identity);
            spot.transform.SetParent(SpotContainer.transform);

            spot.transform.localEulerAngles = Vector3.zero;

            AllSpots.Add(spot);
        }
    }
}
