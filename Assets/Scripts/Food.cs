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
        float[][] ranges = foodValues.getRanges();
        float[] weights = foodValues.getWeights();
        float base_output = foodValues.getBaseOutput();
        need_values = new float[needs.Length];
        //TO-DO
        for(int i = 0; i < weights.Length; i++){
            if(weights[i] > 0){ //Lazy evaluation, only detect if it matters
                //Determine need values
                switch (needs[i]){
                    case "Terrain":
                        break;
                    case "GasX":
                        break;
                    case "GasY":
                        break;
                    case "GasZ":
                        break;
                    case "Temperature":
                        break;
                    case "Lighting":
                        break;
                    case "Liquid":
                        break;
                    case default:
                        print("Error: No need name matches.");
                        break;
                }
                
            }
        }
    }
}
