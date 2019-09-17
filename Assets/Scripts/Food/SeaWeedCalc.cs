using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedCalc
{
    private float baseOutput = 5;
    private int size = 1;
    private float totalWeight = 10;
    private Vector2 position;

    private Dictionary<string, int[]> blue = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { -1, -1, -1, -1 } },
        { "mod", new int[] { 0, 49, -1, -1 } },
        { "good", new int[] { 50, 100, -1, -1 } }
    };

    private Dictionary<string, int[]> green = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { -1, -1, -1, -1 } },
        { "mod", new int[] { 0, 49, -1, -1 } },
        { "good", new int[] { 50, 100, -1, -1 } }
    };

    private Dictionary<string, int[]> x = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 0, 30, -1, -1 } },
        { "mod", new int[] { 31, 40, -1, -1 } },
        { "good", new int[] { 41, 100, -1, -1 } }
    };

    private Dictionary<string, int[]> y = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 0, 10, -1, -1 } },
        { "mod", new int[] { -1, -1, -1, -1 } },
        { "good", new int[] { 11, 100, -1, -1 } }
    };

    private Dictionary<string, int[]> temp = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 51, 100, -1, -1 } },
        { "mod", new int[] { 21, 50, 81, 90 } },
        { "good", new int[] { 0, 20, -1, -1 } }
    };
    private Dictionary<string, int[]> light = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 0, 40, -1, -1 } },
        { "mod", new int[] { 41, 80, -1, -1 } },
        { "good", new int[] { 81, 100, -1, -1 } }
    };
    private Dictionary<string, float> weights = new Dictionary<string, float>()
    {
        { "blue", 3},
        { "green", 3 },
        { "x", 1 },
        { "y", 0.5f },
        { "temp", 2 },
        { "light", 0.5f }
    };


    public int modifyOutput(int blueVal, int greenVal, int xVal, int yVal, int tempVal, int lightVal)
    {
        float modifiedOutput = baseOutput;

        float blueResult = needResult(blue, blueVal, "blue");
        modifiedOutput += blueResult;

        Debug.Log("modified output: " + modifiedOutput);

        float greenResult = needResult(green, greenVal, "green");
        modifiedOutput += greenResult;

        Debug.Log("modified output: " + modifiedOutput);

        float xResult = needResult(x, xVal, "x");
        modifiedOutput += xResult;
        Debug.Log("modified output: " + modifiedOutput);


        float yResult = needResult(y, yVal, "y");
        modifiedOutput += yResult;
        Debug.Log("modified output: " + modifiedOutput);

        float tempResult = needResult(temp, tempVal, "temp");
        modifiedOutput += tempResult;
        Debug.Log("modified output: " + modifiedOutput);

        float lightResult = needResult(light, lightVal, "light");
        modifiedOutput += lightResult;
        Debug.Log("modified output: " + modifiedOutput);

        //print("MOD OUTPUT IS: "  + modifiedOutput);
        return ((int)Mathf.Floor(modifiedOutput));
    }

    private float needResult(Dictionary<string, int[]> needDict, int value, string needWeight)
    {
        float modifier = 0;

        Debug.Log("needDict: " + needDict);
        Debug.Log("value: " + value);
        Debug.Log("needWeight: " + needWeight);

        if (value >= needDict["bad"][0] && value <= needDict["bad"][1] || value >= needDict["bad"][2] && value <= needDict["bad"][3])
        {
            modifier = -((weights[needWeight] / totalWeight) * baseOutput);
            Debug.Log("modifier bad: " + modifier);
        }

        if (value >= needDict["good"][0] && value <= needDict["good"][1] || value >= needDict["good"][2] && value <= needDict["good"][3])
        {
            modifier = (weights[needWeight] / totalWeight) * baseOutput;
            Debug.Log("modifier good: " + modifier);
        }

        Debug.Log("returning: " + modifier);
        return modifier;

    }

}
