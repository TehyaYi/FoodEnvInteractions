using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	[SerializeField] private float total_weight; 
	[SerializeField] private float base_output;
	public string[] needs = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};
	[SerializeField] private float[] weights;

	//Values in conditions[] represents how good 
	//the needs are met: 0 = bad, 1 = moderate, 2 = good
	[SerializeField] private int[] conditions; 

    //variables for the table
    [SerializeField] private float[] terrain_ranges = new float[4]; //[goodLow, goodHigh, modLow, modHigh]
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

    	print("total_output: " + Calculate_Output());
    }

    float Calculate_Output(){
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
