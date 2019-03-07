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

    public GameObject MessageBarPrefab;
    public GameObject MessageContainer;

    public List<MessageProperties> AllMessages;

    private void Update()
    {
        // TIJDELIJK
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddMessageToPhone();
        }
    }

    public void AddMessageToPhone()
    {
        GameObject bar = (GameObject)Instantiate(MessageBarPrefab, Vector3.zero, Quaternion.identity);
        bar.transform.SetParent(MessageContainer.transform);

        bar.transform.localEulerAngles = Vector3.zero;
        bar.transform.localScale = Vector3.one;

        RectTransform rect = bar.GetComponent<RectTransform>();

        rect.localPosition = new Vector3(0, -24.4f, 0);
        if(AllMessages.Count > 0)
        {
            print(AllMessages[AllMessages.Count - 1].gameObject.transform.position.y);

            rect.localPosition = new Vector3(0, AllMessages[AllMessages.Count - 1].gameObject.GetComponent<RectTransform>().localPosition.y + AllMessages[AllMessages.Count - 1].MessageHeight, 0);
        }

        AllMessages.Add(bar.GetComponent<MessageProperties>());
    }
}
