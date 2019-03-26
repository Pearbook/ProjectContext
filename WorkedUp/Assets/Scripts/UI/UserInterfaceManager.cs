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
    public List<GameObject> AllStars;


    [Header("Tasklist")]
    public CanvasGroup TasklistCanvas;
    public List<GameObject> Checkmarks;
    public Animator TasklistAnimator;
    private bool isOpen;

    private void Start()
    {
        ToggleTasklist();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 1"))
        {
            ToggleTasklist();
        }
    }

    void ToggleTasklist()
    {
        if (!isOpen)
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
        // Disable other UI elements
        GameplayManager.Gameplay.TimeIndicator.alpha = 0;
        MobileManager.Mobile.Phone.ForceClose();
        TasklistCanvas.alpha = 0;

        MotivationGroup.alpha = 0;
        ScoreGroup.alpha = 1;
        ScoreGroup.interactable = true;

        if (PlayerManager.Player.Stats.Score > (30 / 3)) // One Star
        {
            AllStars[0].SetActive(true);
            AllStars[3].SetActive(false);
        }
        if (PlayerManager.Player.Stats.Score > (30 / 3) * 2) // Two Stars
        {
            AllStars[1].SetActive(true);
            AllStars[4].SetActive(false);
        }
        if (PlayerManager.Player.Stats.Score >= (30 / 3) * 3) // Three Stars
        {
            AllStars[2].SetActive(true);
            AllStars[5].SetActive(false);
        }


        StarContainer.SetActive(true);
        ScoreDisplay.text = PlayerManager.Player.Stats.Score.ToString();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void SpawnErrorIndicator(Transform target, bool goodOrBad)
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load("UI/obj_errorIndicator"), target.position, Quaternion.identity);

        obj.GetComponent<ErrorIndicatorSettings>().DisplayIndicator(goodOrBad);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
