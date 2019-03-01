using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public enum type
    {
        CookingPot,
        WashingMachine,
        BookCase
    }

    public type InteractableType;

    public float TimerLimit;

    private float timer;
    private float seconds;
    private float dist;
    private bool waitForTimer;
    private bool isTimerDone;

    public GameObject PrefabToSpawn;

    [Header("Container")]
    public int ItemLimit;
    private int currentItemCount;

    [Header("UI")]
    public CanvasGroup InstructionGroup;
    public CanvasGroup BarCanvas;
    public Image ProgressBar;
    public CanvasGroup Checkmark;

    [Header("Other")]
    public PingPongScale Scale;

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    void Update()
    {
        if (timer == 0)
        {
            BarCanvas.alpha = 0;

            if (!isTimerDone)
            {
                dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

                if (InteractableType == type.CookingPot)
                {
                    if (dist < 2)
                        InstructionGroup.alpha = 1;
                    else
                        InstructionGroup.alpha = 0;
                }

                if (InteractableType == type.WashingMachine)
                {
                    if (PlayerManager.Player.Controller.holdObj != null && PlayerManager.Player.Controller.holdObj.tag == "laundry")
                    {
                        if (dist < 2)
                            InstructionGroup.alpha = 1;
                        else
                            InstructionGroup.alpha = 0;
                    }
                    else
                    {
                        if (currentItemCount > 0)
                        {
                            if (dist < 2)
                                InstructionGroup.alpha = 1;
                            else
                                InstructionGroup.alpha = 0;
                        }
                        else
                            InstructionGroup.alpha = 0;
                    }
                }
            }
        }
        else
        {
            BarCanvas.alpha = 1;
            InstructionGroup.alpha = 0;
        }

        if(isTimerDone)
        {
            if(Checkmark != null)
                Checkmark.alpha = 1;
        }
        else
        {
            if (Checkmark != null)
                Checkmark.alpha = 0;
        }

        if (InteractableType == type.CookingPot)
        {
            if (seconds >= TimerLimit)
            {
                Action();
                seconds = 0;
                timer = 0;

                PlayerManager.Player.Controller.isInteracting = false;
                PlayerManager.Player.Controller.allowMovement = true;
            }
            else
            {
                ProgressBar.fillAmount = Custom.ReturnFillAmount(seconds, TimerLimit);
            }
        }

        if(InteractableType == type.WashingMachine)
        {
            if(waitForTimer)
            {
                if (seconds >= TimerLimit)
                {
                    isTimerDone = true;

                    seconds = 0;
                    timer = 0;
                    currentItemCount = 0;
                    waitForTimer = false;
                }
                else
                {
                    UpdateTimer();

                    ProgressBar.fillAmount = Custom.ReturnFillAmount(seconds, TimerLimit);
                }
            }
        }
    }

    public void DoInteraction()
    {
        if (InteractableType == type.CookingPot)
            UpdateTimer();
        if (InteractableType == type.WashingMachine)
            InputItem();

    }

    // WASHING MACHINE

    void InputItem()
    {
        if (isTimerDone == false)
        {
            if (PlayerManager.Player.Controller.holdObj != null)
            {
                GameObject obj = PlayerManager.Player.Controller.holdObj;

                if (obj.gameObject.tag == "laundry")
                {
                    //PUT IN MACHINE
                    PlayerManager.Player.RemoveHoldItem();
                    currentItemCount++;

                    InstructionGroup.alpha = 0;

                    if (Scale != null)
                        Scale.PingPong();
                }
            }
            else
            {
                if (currentItemCount > 0)
                {
                    // TURN ON THE MACHINE
                    waitForTimer = true;
                }
            }
        }
        else
        {
            //REMOVE FROM MACHINE
            Action();

            if (Scale != null)
                Scale.PingPong();

            isTimerDone = false;
        }
    }

    void Action()
    {
        PlayerManager.Player.GiveHoldItem(PrefabToSpawn, true);
    }
}
