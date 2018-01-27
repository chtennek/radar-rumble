using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public string label = "HP";
    public int minValue = 0;
    public int maxValue = 100;
    public int currentValue = 100; // Set in inspector to initial value

    public Image canvasDisplay;
    public bool overrideColor = false;
    public Color[] displayColor;
    public bool useColorGradient = false;

    public void AddValue(int value)
    {
        currentValue = Mathf.Min(maxValue, currentValue + value);
    }

    public void SubtractValue(int value)
    {
        currentValue = Mathf.Max(minValue, currentValue - value);
    }

    public void SetValue(int value)
    {
        currentValue = Mathf.Clamp(currentValue + value, minValue, maxValue);
    }

    public void AddPercent(float percent)
    {
        AddValue((int)Mathf.Floor(percent * maxValue));
    }

    public void SubtractPercent(float percent)
    {
        SubtractValue((int)Mathf.Floor(percent * maxValue));
    }

    public void SetPercent(float percent)
    {
        SetValue((int)Mathf.Floor(percent * maxValue));
    }


    private void Update()
    {
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
        if (canvasDisplay != null)
        {
            canvasDisplay.fillAmount = GetValuePercent();
            if (overrideColor)
            {
                canvasDisplay.color = DetermineDisplayColor();
            }
        }
    }

    private float GetValuePercent()
    {
        return (maxValue == 0) ? 1 : (float)currentValue / maxValue;
    }

    private void SetValuePercent(float percent)
    {
        currentValue = (int)(percent * maxValue);
    }

    private Color DetermineDisplayColor()
    {
        if (displayColor.Length == 0)
            return Color.clear;
        if (displayColor.Length == 1)
            return displayColor[0];
        if (currentValue == maxValue)
            return displayColor[displayColor.Length - 1];

        // Find where we are in displayColor[]
        float percent = GetValuePercent();
        int buckets = (useColorGradient) ? displayColor.Length - 1 : displayColor.Length;
        int index = (int)Mathf.Floor(percent * buckets);
        float percentInBucket = (percent * buckets) % 1;

        if (useColorGradient)
        {
            return Color.Lerp(displayColor[index], displayColor[index + 1], percentInBucket);
        }
        else
        {
            return displayColor[index];
        }
    }
}
