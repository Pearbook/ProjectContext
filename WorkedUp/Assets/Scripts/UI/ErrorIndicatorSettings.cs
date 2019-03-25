using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorIndicatorSettings : MonoBehaviour
{
    public CanvasGroup DoneIndicatorCanvas;
    public CanvasGroup ErrorIndicatorCanvas;

    public void DisplayIndicator(bool isGood)
    {
        if (isGood)
            DoneIndicatorCanvas.alpha = 1;
        else
            ErrorIndicatorCanvas.alpha = 1;
    }
}
