using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private static int numOfNeeds = 10;
    
    [SerializeField] private float base_output = 0;

    public string[] needs = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};

    //How much of each need is provided, raw value of needs
    [SerializeField] private float[] need_values = new float[numOfNeeds];
	[SerializeField] private float[] weights = new float[numOfNeeds];
    private float total_weight;

	//Values in conditions[] represents how good 
	//the needs are met: 0 = bad, 1 = moderate, 2 = good
    //serialized just for debugging
	[SerializeField] private int[] conditions = new int[numOfNeeds]; 

    //representing the ranges as a matrix (table).
    private float[][] ranges;
    //variables for the table, determines what values are good or bad for the plant
    [SerializeField] private float[] terrain_ranges = new float[4]; //[0] = lower bound for good terrain, [1] = upper bound for good terrain, [2] = low bound for mod, [3] = high bound for mod, otherwise bad terrain.
    [SerializeField] private float[] red_liquid_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] blue_liquid_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] green_liquid_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] black_liquid_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] gasX_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] gasY_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] gasZ_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] temperature_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    [SerializeField] private float[] lighting_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
    

    // Start is called before the first frame update
    void Start()
    {
    	total_weight = 0;
    	for(int i = 0; i < weights.Length; i++){
            //sum all values in weights to get total weight
    		total_weight += weights[i];
    	}

        ranges = new float[][]{     terrain_ranges, 
                                    red_liquid_ranges, 
                                    blue_liquid_ranges,
                                    black_liquid_ranges,
                                    gasX_ranges,
                                    gasY_ranges,
                                    gasZ_ranges, 
                                    temperature_ranges, 
                                    lighting_ranges     };

    	print("total_output: " + CalculateOutput());
    }

    //Detects what is in the environment and populate need_values[]
    void DetectEnvironment(){
        //TO-DO
    }

    //Calculate all conditions, i.e. how much the environment satisfies its needs
    void CalculateConditions(){
        for(int i = 0; i < numOfNeeds; i++){
            CalculateCondition(i, need_values[i]);
        }
    }

    //Calculate how good the need are met and it them in conditions[]
    //needIndex = index of the need we are addressing, val = current value of the need
    void CalculateCondition(int needIndex, float val){
        int ni = needIndex;
        if(val >= ranges[ni][0]&&val <= ranges[ni][1]&&
            !(ranges[ni][0] == 0&&ranges[ni][1] == 0)){//upper and lower being 0 means no value satisfies
            conditions[ni] = 2;
        }else if(val >= ranges[ni][2]&&val <= ranges[ni][3]&&
            !(ranges[ni][2] == 0&&ranges[ni][3] == 0)){//upper and lower being 0 means no value satisfies
            conditions[ni] = 1;
        }else{
            conditions[ni] = 0;
        }
        return;
    }

    //returns the final output of the plant
    float CalculateOutput(){
        float total_output = 0;
        for(int i = 0; i < needs.Length; i++){
            //total output of each need is weight of the need/total weight * condition (bad = 0, med = 1, good = 2) * base output of the plant
            total_output += conditions[i] * weights[i]/total_weight * base_output;
        }
        return total_output;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
