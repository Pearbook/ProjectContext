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
    public bool IsResting;
    public float IncreaseMultiplier = 1;
    public float DecreaseMultiplier = 1;

    [Header("Particle")]
    public ParticleSystem SweatParticles;

    [Header("UI")]
    public Image MotivationBar;

    private void Start()
    {
        CurrentPlayerMotivation = MaxPlayerMotivation;
    }

    private void Update()
    {
        MotivationBar.fillAmount = Custom.ReturnFillAmount(CurrentPlayerMotivation, MaxPlayerMotivation);

        // BELOW 50% SLOWER MOVEMENTSPEED
        if (CurrentPlayerMotivation < 50)
        {
            PlayerManager.Player.Controller.SpeedModifier = 0.5f;
            MotivationBar.color = new Color32(255, 216, 0, 255);    //Yellow

            //Play sweat particles
            if (!SweatParticles.isPlaying)
                SweatParticles.Play();
        }
        else if (CurrentPlayerMotivation > 50)
        {
            PlayerManager.Player.Controller.SpeedModifier = 1;
            MotivationBar.color = new Color32(89, 187, 69, 255);    //Green

            //Stop sweat particles
            if (SweatParticles.isPlaying)
                SweatParticles.Stop();
        }

        // BLOW 25% CANT CARRY STUFF
        if (CurrentPlayerMotivation < 25)
        {
            MotivationBar.color = new Color32(219, 40, 40, 255);    //Red
        }

        if (!IsResting)
        {
            if (CurrentPlayerMotivation <= 0)
                CurrentPlayerMotivation = 0;
            else
                DecreaseEnergy();
        }
        else
        {
            if (CurrentPlayerMotivation <= MaxPlayerMotivation)
                IncreaseEnergy();
        }
    }

    public void DecreaseEnergy()
    {
        CurrentPlayerMotivation -= DecreaseMultiplier * Time.deltaTime;
    }

    public void IncreaseEnergy()
    {
        CurrentPlayerMotivation += IncreaseMultiplier * Time.deltaTime;
    }
}
