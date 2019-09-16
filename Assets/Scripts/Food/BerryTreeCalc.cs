using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryTreeCalc
{
	private float baseOutput = 20;
	private int size = 4;
	private int rock = 0;
	private int sand = 1;
	private int dirt = 2;
	private int grass = 4;
	private float totalWeight = 10;
	private Vector2 position;
	private Dictionary<string, int> tiles = new Dictionary<string, int>();
	public Dictionary<string, int[]> terrain = new Dictionary<string, int[]>()
	{
		{"bad", new int[] { 0, 4, -1, -1 }},
		{"mod", new int[] { 5, 9, -1, -1 }},
		{"good", new int[] { 10, 16, -1, -1}}
	};
	private Dictionary<string, int[]> y = new Dictionary<string, int[]>()
	{
		{ "bad", new int[] { 0, 10, -1, -1 } },
		{ "mod", new int[] { -1, -1, -1, -1 } },
		{ "good", new int[] { 11, 100, -1, -1 } }
	};
	private Dictionary<string, int[]> z = new Dictionary<string, int[]>()
	{
		{ "bad", new int[] { 0, 30, -1, -1 } },
		{ "mod", new int[] { 31, 40, -1, -1 } },
		{ "good", new int[] { 41, 100, -1, -1 } }
	};
	private Dictionary<string, int[]> temp = new Dictionary<string, int[]>()
	{
		{ "bad", new int[] { 0, 30, -1, -1 } },
		{ "mod", new int[] { 31, 40, 91, 100 } },
		{ "good", new int[] { 41, 90, -1, -1 } }
	};
	private Dictionary<string, int[]> light = new Dictionary<string, int[]>()
	{
		{ "bad", new int[] { 0, 10, -1, -1 } },
		{ "mod", new int[] { 11, 20, -1, -1 } },
		{ "good", new int[] { 21, 100, -1, -1 } }
	};

	private Dictionary<string, float> weights = new Dictionary<string, float>()
	{
		{ "terrain", 4.0f },
		{ "y", 0.5f },
		{ "z", 0.5f },
		{ "temp", 2.0f },
		{ "light", 3.0f }
	};

	public int modifyOutput(int yVal, int zVal, int tempVal, int lightVal, Dictionary<string, int> tiles)
	{
		float modifiedOutput = baseOutput;

		int terrainScore = terrainNeed(tiles);
		float terrainResult = needResult(terrain, terrainScore, "terrain");
		modifiedOutput += terrainResult;

		Debug.Log("modified output: " + modifiedOutput);

		float yResult = needResult(y, yVal, "y");
		modifiedOutput += yResult;
		Debug.Log("modified output: " + modifiedOutput);

		float zResult = needResult(z, zVal, "z");
		modifiedOutput += zResult;
		Debug.Log("modified output: " + modifiedOutput);

		float tempResult = needResult(temp, tempVal, "temp");
		modifiedOutput += tempResult;
		Debug.Log("modified output: " + modifiedOutput);

		float lightResult = needResult(light, lightVal, "light");
		modifiedOutput += lightResult;
		Debug.Log("modified output: " + modifiedOutput);

		//print("MOD OUTPUT IS: " + modifiedOutput);
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

	private int terrainNeed(Dictionary<string, int> tiles)
	{
		return tiles["rock"] * rock + tiles["sand"] * sand + tiles["dirt"] * dirt + tiles["grass"] * grass;
	}

}
