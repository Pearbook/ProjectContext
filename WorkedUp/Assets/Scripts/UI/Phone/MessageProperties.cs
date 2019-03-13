using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageProperties : MonoBehaviour
{ 
    public Text TextObject;

    public RectTransform MessageRect;
    public RectTransform PanelRect;

    public Image ProfileImage;

    public float MessageHeight;

    public void SetText(string txt, float height, Sprite sprite)
    {
        TextObject.text = txt;
        SetHeight(height);

        if(sprite != null)
            SetProfile(sprite);
    }

    void SetHeight(float newheight)
    {
        MessageHeight = newheight;

        PanelRect.sizeDelta = new Vector2(PanelRect.sizeDelta.x, newheight);

        MessageRect.sizeDelta = new Vector2(MessageRect.sizeDelta.x, newheight);
    }

    void SetProfile(Sprite newSprite)
    {
        ProfileImage.sprite = newSprite;
    }
}
