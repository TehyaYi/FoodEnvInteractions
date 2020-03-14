﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoodSource : MonoBehaviour
{
    //using enum to create a dropdown list
    public enum FoodTypes { SpaceMaple, Food2, Food3, Food4, Food5 };
    [SerializeField] private FoodTypes type;

    //ScriptableObject to read from
    public FoodScriptableObject foodValues;

    // For debugging, might be removed later
    //How much of each need is provided, raw value of needs
    [SerializeField] private float[] rawValues;

    //How well each need is provided
    [SerializeField] private int[] conditions;

    [SerializeField] private float totalOutput;

    public GameManager gameManager;

    public float[] getRawValues() { return rawValues; }
    public float getOutput() { return totalOutput; }
    public FoodTypes getType() { return type; }

    public TileRetriever tileRetriever;

    // Start is called before the first frame update
    void Start()
    {
        int numNeeds = foodValues.getNSO().Length;
        rawValues = new float[numNeeds];
        conditions = new int[numNeeds];
        totalOutput = 0;

        if (foodValues == null) {
            print("Error: foodValues is not set");
        }

        DetectEnvironment();
        print("total_output: " + totalOutput);
    }

    /// <summary>
    /// Detects what is in the environment and populate rawValues[].
    /// </summary>
    public void DetectEnvironment()
    {
        NeedScriptableObject[] rso = foodValues.getNSO();
        float[] weights = foodValues.getWeights();
        string[] needs = foodValues.getNeeds();

        //TODO Implement liquid
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] > 0)
            { //Lazy evaluation, only detect if it matters
                //Determine need values
                switch (needs[i])
                {
                    case "Terrain":
                        //get tiles around the food source and return as an array of integers
                        TerrainTile[] terrainTiles = tileRetriever.GetTiles(transform.position, foodValues.getRadius()).ToArray();

                        //quick check for no tiles read
                        if (terrainTiles.Length == 0) { rawValues[i] = 0; break; }

                        List<TileType> tiles = new List<TileType>();

                        foreach (TerrainTile tile in terrainTiles) {
                            tiles.Add(tile.type);
                        }
                        //maybe consider swapping tiles.length for (1+2*radius+2*radius*radius) i.e. 1, 5, 13, 25, ...
                        //because less space might suggest worse terrain for plant (as its roots have to be more crammed and get less resource overall)
                        float avgValue = ((TerrainNeedScriptableObject)rso[i]).getValue(tiles.ToArray()) / tiles.Count;
                        rawValues[i] = avgValue;
                        break;
                    case "Gas X":
                        //Read value from some class that handles atmosphere
                        rawValues[i] = gameManager.getGX();
                        break;
                    case "Gas Y":
                        //Read value from some class that handles atmosphere
                        rawValues[i] = gameManager.getGY();
                        break;
                    case "Gas Z":
                        //Read value from some class that handles atmosphere
                        rawValues[i] = gameManager.getGZ();
                        break;
                    case "Temperature":
                        //Read value from some class that handles temperature
                        rawValues[i] = gameManager.getTemp();
                        break;
                    case "Liquid":
                        //TO-DO
                        //get liquid tiles around the food source and return as an array of tiles
                        //find some way to calculate the value if there are two bodies of water
                        float[,] liquid = new float[,] { { 1, 1, 0 }, { 0.5f, 0.5f, 0.5f }, { 0.2f, 0.8f, 0.4f } };

                        rawValues[i] = ((LiquidNeedScriptableObject)rso[i]).getValue(liquid);
                        break;
                    default:
                        Debug.LogError("Error: No need name matches.");
                        break;
                }
                conditions[i] = rso[i].calculateCondition(rawValues[i]);
            }
        }

        //calculate output based on conditions
        totalOutput = FoodOutputCalculator.CalculateOutput(foodValues, conditions);
    }
}
