using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockBehaviour : MonoBehaviour
{
    public Transform ClockHand;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;

    [Header("UI")]
    public Image CircleTimer;

    void Update()
    {
        ClockHand.localEulerAngles = new Vector3(0, 0, -Custom.ReturnFillAmountRadial(GameplayManager.Gameplay.GetCurrentGameplayTime(), 60 * GameplayManager.Gameplay.TimeLimitMinutes));

        CircleTimer.fillAmount = Custom.ReturnFillAmountBackwards(GameplayManager.Gameplay.GetCurrentGameplayTime(), 60 * GameplayManager.Gameplay.TimeLimitMinutes);
    }
}
