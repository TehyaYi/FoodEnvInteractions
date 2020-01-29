using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOutputCalculator : MonoBehaviour
{
    // public static int[] CalculateConditions(FoodScriptableObject fso, float[] values){

    // }
    public static float CalculateOutput(float base_output, float[] weights, float[][] ranges, float[] values){
    	float total_weight = 0;
    	float total_output = 0;
    	//Calculate total weight
    	for(int i = 0; i < weights.Length; i++){
    		total_weight += weights[i];
    	}
    	//Calculate conditions
    	int[] conditions = CalculateConditions(ranges, values);

    	//Calculate total output once we have weights, ranges, values, and conditions
        for(int i = 0; i < ranges.Length; i++){
            //total output of each need is weight of the need/total weight * condition (bad = 0, med = 1, good = 2) * base output of the plant
            total_output += conditions[i] * weights[i]/total_weight * base_output;
        }
        return total_output;
    }

    //Calculate how good the need are met and return as int[] condition
	//0 = bad, 1 = moderate, 2 = good
    //values = current values of the needs
    public static int[] CalculateConditions(float[][] ranges, float[] values){
    	int[] conditions = new int[ranges.Length];
		for(int i = 0; i < ranges.Length; i++){
            print(i);
			float val = values[i];
	        if(val >= ranges[i][0]&&val <= ranges[i][1]&&
	            !(ranges[i][0] == 0&&ranges[i][1] == 0)){//upper and lower being 0 means no value satisfies
	            conditions[i] = 2;
	        }else if(val >= ranges[i][2]&&val <= ranges[i][3]&&
	            !(ranges[i][2] == 0&&ranges[i][3] == 0)){//upper and lower being 0 means no value satisfies
	            conditions[i] = 1;
	        }else{
	            conditions[i] = 0;
	        }
	    }
        return conditions;
    }
}