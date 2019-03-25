using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    [Header("Distance")]
    public float Range;
    private float dist;

    [Header("UI")]
    public CanvasGroup InstructionGroup;

    [Header("Properties")]
    public GameObject BookSlotContainer;
    public List<GameObject> AllBookSlots;
    public GameObject BookPrefab;
    private int slotIndex;

    [Header("Particle Effect")]
    public ParticleSystem SmokePuff;

    [Header("Other")]
    public PingPongScale Scale;

    [Header("Score")]
    public int ScorePerBook;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, PlayerManager.Player.PlayerObject.transform.position);

        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "books")
            {
                if (dist < Range)
                    InstructionGroup.alpha = 1;
                else
                    InstructionGroup.alpha = 0;
            }
        }

    }

    public void AddToBookcase()
    {
        GameObject obj = PlayerManager.Player.Controller.holdObj;
        PlayerManager.Player.Controller.holdObj = null;

        obj.GetComponent<BoxCollider>().enabled = false;
        obj.transform.parent = BookSlotContainer.transform;

        obj.transform.localEulerAngles = Vector3.zero;

        if (slotIndex < AllBookSlots.Count)
        {
            obj.transform.position = AllBookSlots[slotIndex].transform.position;
            slotIndex++;
        }

        InstructionGroup.alpha = 0;

        // ERROR INDICATOR
        UserInterfaceManager.UI.SpawnErrorIndicator(transform, true);

        // ADD SCORE
        GameplayManager.Gameplay.AddScore(ScorePerBook);

        // REMOVE BOOK FROM LIST
        if (GameplayManager.Gameplay.AllBooks.Contains(obj))
            GameplayManager.Gameplay.AllBooks.Remove(obj);

        if (!SmokePuff.isPlaying)
            SmokePuff.Play();

        if (Scale != null)
            Scale.PingPong();
    }

    public void EmptyBookcase()
    {
        if (slotIndex > 0)
        {
            for (int i = 0; i < slotIndex; ++i)
            {
                GameObject child = BookSlotContainer.transform.GetChild(i).gameObject;
                GameObject book = (GameObject)Instantiate(BookPrefab, new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z), Quaternion.identity);

                // ADD BOOKS TO LIST
                GameplayManager.Gameplay.AllBooks.Add(book);

                // REMOVE SCORE
                GameplayManager.Gameplay.AddScore(-ScorePerBook);

                Destroy(child);
            }

            slotIndex = 0;
        }

        if (Scale != null)
            Scale.PingPong();
    }

    public void Interaction()
    {
        if (PlayerManager.Player.Controller.holdObj != null)
        {
            if (PlayerManager.Player.Controller.holdObj.tag == "books")
            {
                AddToBookcase();
            }
        }
    }
}
