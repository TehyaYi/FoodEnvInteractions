using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    // Inspector provides percentage
    [Range(0, 100)]
    [SerializeField] public float red;
    [Range(0, 100)]
    [SerializeField] public float green;
    [Range(0, 100)]
    [SerializeField] public float blue;

    private Renderer _renderer;

    private void OnValidate()
    {
        if (float.IsNaN(red)) red = 33.33f;
        if (float.IsNaN(green)) green = 33.33f;
        if (float.IsNaN(blue)) blue = 33.33f;
        float total = red + green + blue;
        if (total != 100 && total != 0)
        {
            red = 100 * (red / total);
            Debug.Log("red" + total);
            green = 100 * (green / total);
            blue = 100 * (blue / total);
        }
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
