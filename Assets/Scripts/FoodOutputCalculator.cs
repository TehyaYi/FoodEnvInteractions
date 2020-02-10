using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOutputCalculator : MonoBehaviour
{
    public static float CalculateOutput(FoodScriptableObject fso, int[] conditions){
        return CalculateOutput(fso.getBaseOutput(), fso.getWeights(), fso.getTWeight(), conditions);
    }
    //values = raw values, tWeight = total weight
    public static float CalculateOutput(float base_output, float[] weights, float tWeight, int[] conditions){
    	float total_weight = tWeight;
    	float total_output = 0;
    	////Calculate total weight
    	//for(int i = 0; i < weights.Length; i++){
    	//	total_weight += weights[i];
    	//}

    	//Calculate total output once we have weights, ranges, values, and conditions
        for(int i = 0; i < weights.Length; i++){
            //total output of each need is weight of the need/total weight * condition (bad = 0, med = 1, good = 2) * base output of the plant
            total_output += conditions[i] * weights[i]/total_weight * base_output;
        }
        return total_output;
    }
}