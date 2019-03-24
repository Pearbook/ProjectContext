using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    static UserInterfaceManager userInterface;

    public static UserInterfaceManager UI
    {
        get
        {
            if (userInterface == null)
            {
                userInterface = FindObjectOfType<UserInterfaceManager>();
                if (userInterface == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    userInterface = obj.AddComponent<UserInterfaceManager>();
                }
            }
            return userInterface;
        }
    }

    [Header("Motivation Bar")]
    public CanvasGroup MotivationGroup;

    [Header("Score Screen")]
    public CanvasGroup ScoreGroup;
    public Text ScoreDisplay;
    public GameObject StarContainer;

    [Header("Tasklist")]
    public List<GameObject> Checkmarks;
    public Animator TasklistAnimator;
    private bool isOpen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isOpen)
            {
                isOpen = true;

                TasklistAnimator.SetBool("Open", true);
            }
            else
            {
                isOpen = false;

                TasklistAnimator.SetBool("Open", false);
            }
        }
    }

    public void ActivateCheckmark(int index)
    {
        Checkmarks[index].SetActive(true);
    }

    public void DisableCheckmark(int index)
    {
        Checkmarks[index].SetActive(false);
    }

    public void DisplayScoreScreen()
    {
        MotivationGroup.alpha = 0;
        ScoreGroup.alpha = 1;
        ScoreGroup.interactable = true;

        StarContainer.SetActive(true);
        ScoreDisplay.text = PlayerManager.Player.Stats.Score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
