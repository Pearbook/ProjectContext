using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Score")]
    public int Score;

    [Header ("Properties")]
    public float MaxPlayerMotivation;
    public float CurrentPlayerMotivation;

    [Header("UI")]
    public Image MotivationBar;

    private void Update()
    {
        MotivationBar.fillAmount = Custom.ReturnFillAmount(CurrentPlayerMotivation, MaxPlayerMotivation);
    }

    public void DecreaseMotivation(float amount)
    {
        CurrentPlayerMotivation -= amount;
    }
}
