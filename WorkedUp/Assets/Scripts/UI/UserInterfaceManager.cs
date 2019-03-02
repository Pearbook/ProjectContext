using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void DisplayScoreScreen()
    {
        MotivationGroup.alpha = 0;
        ScoreGroup.alpha = 1;

        ScoreDisplay.text = PlayerManager.Player.Stats.Score.ToString();
    }
}
