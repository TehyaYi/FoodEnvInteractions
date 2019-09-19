using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    // Inspector provides percentage
    [Range(0, 100)]
    [SerializeField] public float red;
    private float lastRed;
    [Range(0, 100)]
    [SerializeField] public float green;
    private float lastGreen;
    [Range(0, 100)]
    [SerializeField] public float blue;
    private float lastBlue;

    private Renderer _renderer;

    private void OnValidate()
    {
        if (float.IsNaN(red)) red = 33.33f;
        if (float.IsNaN(green)) green = 33.33f;
        if (float.IsNaN(blue)) blue = 33.33f;
        float total = red + green + blue;
        
        if(lastRed != red)
        {
            float split = 100 - red;
            float greenProp = green / (green + blue);
            float blueProp = blue / (green + blue);
            green = greenProp * split;
            blue = blueProp * split;
        }
        else if(lastGreen != green)
        {
            float split = 100 - green;
            float redProp = red / (red + blue);
            float blueProp = blue / (red + blue);
            red = redProp * split;
            blue = blueProp * split;
        }
        else if (lastBlue != blue)
        {
            float split = 100 - blue;
            float redProp = red / (red + green);
            float greenProp = green / (red + green);
            red = redProp * split;
            green = greenProp * split;
        }

        lastRed = red;
        lastGreen = green;
        lastBlue = blue;
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _renderer.material.color = new Color(ColorValFromPercent(red), ColorValFromPercent(green), ColorValFromPercent(blue), 1);
    }

    private float ColorValFromPercent(float percent)
    {

        float result = percent / 100f;
        return result;
    }
}
