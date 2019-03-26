using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileManager : MonoBehaviour
{
    static MobileManager manager;

    public static MobileManager Mobile
    {
        get
        {
            if (manager == null)
            {
                manager = FindObjectOfType<MobileManager>();
                if (manager == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    manager = obj.AddComponent<MobileManager>();
                }
            }
            return manager;
        }
    }

    public MobilePhone Phone;

    [Header("Timer")]
    public float TimerLimit;

    private float timer;
    private float seconds;

    [Header("Read Timer")]
    public float ReadTimerLimit;

    private float readTimer;
    private float readSeconds;
    public bool allowClose;

    [HideInInspector]
    public bool isOpen;

    [Header("UI")]
    public GameObject MessageBarPrefab;
    public GameObject CommentBarPrefab;
    public GameObject MessageContainer;

    public List<MessageProperties> AllMessages;
    public List<MessageScriptable> AllScriptables;
    public List<MessageScriptable> AllComments;

    [Header("Notification")]
    public CanvasGroup NotificationGroup;

    private int messageIndex;
    private int commentIndex;
    private int phoneIndex;

    [HideInInspector]
    public bool hasComment;

    private void Update()
    {
        if(timer >= TimerLimit)
        {
            if (!isOpen)
            {
                if (phoneIndex < AllComments.Count + AllScriptables.Count)
                {
                    isOpen = true;

                    // Disable Player
                    PlayerManager.Player.DisablePlayer();

                    AddMessageToPhone(false);
                    Phone.TogglePhone();
                }
            }
            else
            {
                if (!allowClose)
                {
                    if (readTimer >= ReadTimerLimit)
                    {
                        allowClose = true;
                        Phone.EnterText.alpha = 1;
                    }
                    else
                        UpdateReadTimer();
                }
            }
        }
        else
        {
            UpdateTimer();
        }

    }

    public void ClosePhone()
    {
        isOpen = false;
        timer = 0;
        seconds = 0;

        readTimer = 0;
        readSeconds = 0;

        allowClose = false;
        Phone.EnterText.alpha = 0;

        PlayerManager.Player.EnablePlayer();
    }

    public void AddMessageToPhone(bool isComment)
    {
        ShowNotification();

        GameObject bar = null;

        if(isComment)
            bar = (GameObject)Instantiate(CommentBarPrefab, Vector3.zero, Quaternion.identity);
        else
            bar = (GameObject)Instantiate(MessageBarPrefab, Vector3.zero, Quaternion.identity);


        bar.transform.SetParent(MessageContainer.transform);

        bar.transform.localEulerAngles = Vector3.zero;
        bar.transform.localScale = Vector3.one;

        RectTransform rect = bar.GetComponent<RectTransform>();
        MessageProperties prop = bar.GetComponent<MessageProperties>();

        rect.localPosition = new Vector3(0, -24.4f, 0);

        if (isComment)
        {
            hasComment = true;

            if (AllComments.Count > 0)
                prop.SetText(AllComments[commentIndex].MyMessage, AllComments[commentIndex].myHeight, null);
        }
        else
        {
            if (AllScriptables.Count > 0)
                prop.SetText(AllScriptables[messageIndex].MyMessage, AllScriptables[messageIndex].myHeight, AllScriptables[messageIndex].Profile);
        }

        if (AllMessages.Count > 0)
        {
            foreach (MessageProperties message in AllMessages)
            {
                message.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, message.gameObject.GetComponent<RectTransform>().localPosition.y + bar.GetComponent<MessageProperties>().MessageHeight, 0);
            }
        }

        AllMessages.Add(prop);

        phoneIndex++;

        if (!isComment)
        {
            messageIndex++;
            hasComment = false;
        }
        else
        {
            commentIndex++;
        }
    }

    void ShowNotification()
    {
        if (!Phone.isOpen)
        {
            NotificationGroup.alpha = 1;

            if (Phone.Scale != null)
                Phone.Scale.PingPong();
        }
    }

    public void HideNotification()
    {
        NotificationGroup.alpha = 0;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
    }

    public void UpdateReadTimer()
    {
        readTimer += Time.deltaTime;
        readSeconds = readTimer % 60;
    }

}
