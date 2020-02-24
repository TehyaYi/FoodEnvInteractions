using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : MonoBehaviour
{
    //using enum to create a dropdown list
    private enum FoodTypes { SpaceMaple, Food2, Food3, Food4, Food5 };
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

    public float getOutput()
    {
        return totalOutput;
    }

    // Start is called before the first frame update
    void Start()
    {
        int numNeeds = foodValues.getNSO().Length;
        rawValues = new float[numNeeds];
        conditions = new int[numNeeds];
        totalOutput = 0;

        if (foodValues != null)
        {
        }
        else
        {
            print("Error: foodValues is null");
        }

        DetectEnvironment();
        print("total_output: " + totalOutput);
    }

    public void UpdateEnv(string need)
    {
        NeedScriptableObject[] rso = foodValues.getNSO();
        for (int i = 0; i < rso.Length; i++) {
            if (rso[i].getName() == need)
            {
                switch (need)
                {
                    case "Terrain":
                        //get tiles around the food source and return as an array of integers
                        //each type of plant should have an id, e.g. 0 = rock, 1 = sand, 2 = dirt, 3 = grass etc.

                        //this is just to demonstrate that it is working
                        Tiles[] tiles = new Tiles[] { Tiles.Rock, Tiles.Rock, Tiles.Grass, Tiles.Grass,
                                    Tiles.Dirt, Tiles.Sand, Tiles.Dirt, Tiles.Dirt }; //2 rocks, 1 sand, 3 dirt, 2 grass
                        float avgValue = ((TerrainNeedScriptableObject)rso[i]).getValue(tiles) / tiles.Length;
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
    }
    //Detects what is in the environment and populate rawValues[]
    void DetectEnvironment()
    {
        NeedScriptableObject[] rso = foodValues.getNSO();
        float[] weights = foodValues.getWeights();
        string[] needs = foodValues.getNeeds();
        //TO-DO
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] > 0)
            { //Lazy evaluation, only detect if it matters
                //Determine need values
                switch (needs[i])
                {
                    case "Terrain":
                        //get tiles around the food source and return as an array of integers
                        //each type of plant should have an id, e.g. 0 = rock, 1 = sand, 2 = dirt, 3 = grass etc.

                        //this is just to demonstrate that it is working
                        Tiles[] tiles = new Tiles[] { Tiles.Rock, Tiles.Rock, Tiles.Grass, Tiles.Grass,
                            Tiles.Dirt, Tiles.Sand, Tiles.Dirt, Tiles.Dirt }; //2 rocks, 1 sand, 3 dirt, 2 grass
                        float avgValue = ((TerrainNeedScriptableObject)rso[i]).getValue(tiles) / tiles.Length;
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
