using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{   
    public FoodScriptableObject foodValues;
    
    //How much of each need is provided, raw value of needs
    [SerializeField] private float[] need_values;
	
    //representing the good/mod/bad ranges of need satisfaction as a matrix (table).
    private float[][] ranges;

    // Start is called before the first frame update
    void Start()
    {
        if(foodValues != null){
            foodValues.init();
        }else{
            print("Error: foodValues is null");
        }
        //DetectEnvironment();
    	print("total_output: " + FoodOutputCalculator.CalculateOutput(foodValues.getBaseOutput(), foodValues.getWeights(), foodValues.getRanges(), need_values));
    }

    //Detects what is in the environment and populate need_values[]
    void DetectEnvironment(){
        string[] needs = foodValues.getNeeds();
        float[] weights = foodValues.getWeights();
        RangeScriptableObject[] rso = foodValues.getRSO();
        need_values = new float[needs.Length];
        //TO-DO
        for(int i = 0; i < weights.Length; i++){
            if(weights[i] > 0){ //Lazy evaluation, only detect if it matters
                //Determine need values
                switch (needs[i]){
                    case "Terrain":
                        //get tiles around the food source and return as an array of integers
                        //each type of plant should have an id, e.g. 0 = rock, 1 = sand, 2 = grass, etc.

                        //this is just to demonstrate that it is working
                        int[] tiles = new int[] { 0, 0, 3, 3, 2, 1, 2, 2 }; //2 rocks, 1 sand, 3 dirt, 2 grass
                        float avgValue = ((TerrainRangeScriptableObject)rso[i]).getValue(tiles)/tiles.Length;
                        need_values[i] = avgValue;
                        break;
                    case "Gas X":
                        //Read value from some class that handles atmosphere
                        need_values[i] = 0;
                        break;
                    case "Gas Y":
                        //Read value from some class that handles atmosphere
                        need_values[i] = 0;
                        break;
                    case "Gas Z":
                        //Read value from some class that handles atmosphere
                        need_values[i] = 0;
                        break;
                    case "Temperature":
                        //Read value from some class that handles temperature
                        need_values[i] = 0;
                        break;
                    case "Liquid":
                        //TO-DO
                        //get liquid tiles around the food source and return as an array of tiles
                        //find some way to calculate the value if there are two bodies of water
                        
                        need_values[i] = 0;
                        break;
                    default:
                        Debug.LogError("Error: No need name matches.");
                        break;
                }
                
            }
        }
    }
}
