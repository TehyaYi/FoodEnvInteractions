using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOutputCalculator : MonoBehaviour
{

    /// <summary>
    /// Calculate the output of a food source given its food scriptable object and conditions
    /// </summary>
    public static float CalculateOutput(FoodScriptableObject fso, NeedCondition[] conditions){
        int[] intConditions = new int[conditions.Length];
        //convert to int, 0 = bad, 1 = neutral, 2 = good
        for (int i = 0; i < conditions.Length; i++) intConditions[i] = (int)conditions[i];
        return CalculateOutput(fso.BaseOutput, fso.Severities, fso.TotalSeverity, intConditions);
    }

    /// <summary>
    /// Calculate the output of a food source given its base output, weights, total weight, and conditions
    /// </summary>
    //values = raw values, tWeight = total weight
    public static float CalculateOutput(float base_output, float[] weights, float tWeight, int[] conditions){
    	float total_weight = tWeight; //just to clarify

        //Calculate total output once we have weights, ranges, values, and conditions
        float total_output = 0;
        for(int i = 0; i < weights.Length; i++){
            //total output of each need is weight of the need/total weight * condition (bad = 0, med = 1, good = 2) * base output of the plant
            total_output += conditions[i] * weights[i]/total_weight * base_output;
        }
        return total_output;
    }
}