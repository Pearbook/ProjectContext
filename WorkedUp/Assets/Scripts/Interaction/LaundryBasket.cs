using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryBasket : MonoBehaviour
{
    public GameObject LaundryPrefab;

    [Header("Particle Effect")]
    public ParticleSystem SmokePuff;

    [Header("Score")]
    public int ScorePerPile;
    private int index;

    [Header ("Other")]
    public PingPongScale Scale;

    public void AddToBasket()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;

        index++;

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

    public void EmptyBasket()
    {
        if(index > 0)
        {
            for(int i = 0; i < index; ++i)
            {
                GameObject obj = (GameObject)Instantiate(LaundryPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 0.5f), Quaternion.identity);
                obj.AddComponent<Rigidbody>();


                index--;

                // REMOVE SCORE
                GameplayManager.Gameplay.AddScore(-ScorePerPile);
            }
        }

        if (Scale != null)
            Scale.PingPong();
    }
}
