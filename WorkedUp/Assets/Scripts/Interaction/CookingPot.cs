using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingPot : MonoBehaviour
{
    // Player interact
    // Turn on "Stove"
    // Turn on timer
    // Timer reached > Show smoke > wait for interact
    // Interact, reset timer

    // If no Interact for too long
    // Burn food (Disable cooking pot)

    // If interact x amount of times
    // Show checkmark icon
    // Interact to pickup pot

    public bool isActive;
    private bool isDisabled;
    private bool cookingDone;
    private bool hasBurned;

    public bool waitForInteract;

    public int InteractionLimit;
    private int interactionAmount;

    [Header("Objects")]
    public GameObject PotObject;
    public GameObject BurnPotObject;
    public ParticleSystem SmokeParticle;
    public GameObject PrefabToSpawn;

    [Header ("Timer")]
    public float TimerLimit;
    public float BurnTimerLimit;

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

    public void Update()
    {
        if (!isDisabled)
        {
            dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

            if (dist < Range)
                InstructionGroup.alpha = 1;
            else
                InstructionGroup.alpha = 0;

            if (!waitForInteract)
            {
                Warning.alpha = 0;
                BarGroup.alpha = 0;

                if (isActive)
                {
                    if (!SmokeParticle.isPlaying)
                        SmokeParticle.Play();
                }
                
                if (seconds >= TimerLimit)
                {
                    seconds = 0;
                    timer = 0;

                    waitForInteract = true;
                }
                else
                {
                    if (isActive)
                    {
                        UpdateTimer();
                        InstructionGroup.alpha = 0;
                    }
                }
            }
            else
            {
                if (!cookingDone)
                {
                    if (SmokeParticle.isPlaying)
                        SmokeParticle.Stop();

                    if (seconds >= BurnTimerLimit)
                    {
                        seconds = 0;
                        timer = 0;

                        BurnFood();
                    }
                    else
                    {
                        if (isActive)
                            UpdateTimer();
                    }

                    BarGroup.alpha = 1;

                    Warning.alpha = 1;
                    ProgressBar.color = new Color32(219, 40, 40, 255);
                    ProgressBar.fillAmount = Custom.ReturnFillAmount(seconds, BurnTimerLimit);
                }
            }

            if (interactionAmount >= InteractionLimit)
            {
                cookingDone = true;
                Checkmark.alpha = 1;
            }
        }
        else
        {
            BarGroup.alpha = 0;
            Warning.alpha = 0;
            InstructionGroup.alpha = 0;

            if (hasBurned)
            {
                PotObject.SetActive(false);
                BurnPotObject.SetActive(true);
            }
        }
    }

    public void BurnFood()
    {
        waitForInteract = false;
        isActive = false;
        hasBurned = true;

        isDisabled = true;
    }

    public void FinalInteraction()
    {
        PotObject.gameObject.SetActive(false);
        Checkmark.alpha = 0;

        PlayerManager.Player.Stats.DecreaseMotivation(10);

        PlayerManager.Player.GiveHoldItem(PrefabToSpawn, true);
        isDisabled = true;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    public void Interaction()
    {
        if (!isDisabled)
        {
            if (!isActive)
                isActive = true;
            else
            {
                if (waitForInteract)
                {
                    if (interactionAmount < InteractionLimit)
                    {
                        interactionAmount++;
                        waitForInteract = false;

                        seconds = 0;
                        timer = 0;
                    }
                    /*else if (interactionAmount >= InteractionLimit)
                    {
                        FinalInteraction();
                    }*/
                }

                if(cookingDone)
                    FinalInteraction();
            }
        }
    }
}
