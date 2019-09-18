using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTreeCalc
{
    private float baseOutput = 15;
    private int size = 4;
    private int rock = 0;
    private int sand = 0;
    private int dirt = 1;
    private int grass = 3;
    private float totalWeight = 3;
    private Vector2 position;
    private Dictionary<string, int> tiles = new Dictionary<string, int>();
    public Dictionary<string, int[]> terrain = new Dictionary<string, int[]>()
    {
        {"bad", new int[] { 0, 4, -1, -1 }},
        {"mod", new int[] { -1, -1, -1, -1 }},
        {"good", new int[] { 5, 12, -1, -1}}
    };

    private Dictionary<string, int[]> temp = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 0, 30, -1, -1 } },
        { "mod", new int[] { 31, 40, 91, 100 } },
        { "good", new int[] { 41, 90, -1, -1 } }
    };
    private Dictionary<string, int[]> light = new Dictionary<string, int[]>()
    {
        { "bad", new int[] { 0, 20, -1, -1 } },
        { "mod", new int[] { -1, -1, -1, -1 } },
        { "good", new int[] { 21, 100, -1, -1 } }
    };
    private Dictionary<string, float> weights = new Dictionary<string, float>()
    {
        { "terrain", 1.0f },
        { "temp", 1.0f },
        { "light", 1.0f }
    };


    public int modifyOutput(int tempVal, int lightVal, Dictionary<string, int> tiles)
    {
        float modifiedOutput = baseOutput;

        int terrainScore = terrainNeed(tiles);
        float terrainResult = needResult(terrain, terrainScore, "terrain");
        modifiedOutput += terrainResult;

        float tempResult = needResult(temp, tempVal, "temp");
        modifiedOutput += tempResult;

        float lightResult = needResult(light, lightVal, "light");
        modifiedOutput += lightResult;

        return ((int)Mathf.Floor(modifiedOutput));
    }

    private float needResult(Dictionary<string, int[]> needDict, int value, string needWeight)
    {
        float modifier = 0;

        if (value >= needDict["bad"][0] && value <= needDict["bad"][1] || value >= needDict["bad"][2] && value <= needDict["bad"][3])
        {
        }

        if (value >= needDict["good"][0] && value <= needDict["good"][1] || value >= needDict["good"][2] && value <= needDict["good"][3])
        {
            modifier = (weights[needWeight] / totalWeight) * baseOutput;
        }

        return modifier;

    }

    private int terrainNeed(Dictionary<string, int> tiles)
    {
        return tiles["rock"] * rock + tiles["sand"] * sand + tiles["dirt"] * dirt + tiles["grass"] * grass;
    }
}
