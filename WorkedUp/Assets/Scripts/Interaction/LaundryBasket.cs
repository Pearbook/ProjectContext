using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryBasket : MonoBehaviour
{
    [Header("Particle Effect")]
    public ParticleSystem SmokePuff;

    [Header("Score")]
    public int ScorePerPile;

    [Header ("Other")]
    public PingPongScale Scale;

    public void AddToBasket()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;

        PlayerManager.Player.RemoveHoldItem();

        // ADD SCORE
        GameplayManager.Gameplay.AddScore(ScorePerPile);

        if (!SmokePuff.isPlaying)
            SmokePuff.Play();

        if (Scale != null)
            Scale.PingPong();
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "laundry")
            {
                if(PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>() != null && PlayerManager.Player.Controller.holdObj.GetComponent<PickUpProperties>().isClean)
                    AddToBasket();
            }
        }
    }
}
