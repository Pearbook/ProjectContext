using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Custom 
{
    public static Vector2 SnappedVector2(Vector2 pos, float size)
    {
        float snapInverse = 1/size;
        float x, y;

        x = Mathf.Round(pos.x * snapInverse) / snapInverse;
        y = Mathf.Round(pos.y * snapInverse) / snapInverse;

        return new Vector2(x, y);
    }

    public static Vector2 WorldMousePosition()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static string IntToDigitalClock(int value)
    {
        int hoursDisplay = (value / 60) / 60;
        int minsDisplay = value / 60;
        int secsDisplay = value;

        if (secsDisplay >= 60)
            secsDisplay = secsDisplay - (minsDisplay * 60);

        if (minsDisplay >= 60)
            minsDisplay = minsDisplay - (hoursDisplay * 60);

        return "Time: " + hoursDisplay.ToString("00") + ":" + minsDisplay.ToString("00") + ":" + secsDisplay.ToString("00");
    }

    public static float ReturnFillAmount (float value, float maxValue)
    {
        return (value - 0) * (1 - 0) / (maxValue - 0) + 0;
    }

    public static float ReturnFillAmountBackwards (float value, float maxValue)
    {
        return (value - 1) * (0 - 1) / (maxValue - 1) + 1;
    }
}
