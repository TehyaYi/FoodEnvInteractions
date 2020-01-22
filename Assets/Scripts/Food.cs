using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	//private so that no one edits this
	public float total_weights; 
	public float base_output;
	public string[] needs;
	public float[] weights;
	//Values in conditions[] represents how good 
	//the needs are met: 0 = bad, 1 = moderate, 2 = good
	public int[] conditions; 

    //variables for the table
    public int[] terrain_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] red_liquid_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] blue_liquid_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] green_liquid_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] black_liquid_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] gasX_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] gasY_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] gasZ_ranges = new int[6]; //[badLow, badHigh, modLow, modHigh, goodLow, goodHigh]
    public int[] temperature_ranges = new int[3]; // val = |temp_given - middle(radius)| [furthest from radius, middle range from radius, closest to radius]
    public int[] lighting_ranges = new int[3]; // val = |lighting_given - middle(radius)| [furthest from radius, middle range from radius, closest to radius]

    public int terrain_weight;
    public int red_liquid_weight;
    public int blue_liquid_weight;
    public int green_liquid_weight;
    public int black_liquid_weight;
    public int gasX_weight;
    public int gasY_weight;
    public int temperature_weight;
    public int lighting_weight;


    // Start is called before the first frame update
    void Start()
    {
        needs = new string[]{"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};

    	total_weights = 0;
    	for(int i = 0; i < weights.Length; i++){
    		total_weights += weights[i];
    	}

    	print("total_output: " + Calculate_Output());
    }

    float Calculate_Output(){
    	float total_output = 0;
    	for(int i = 0; i < needs.Length; i++){
    		total_output += conditions[i] * weights[i]/total_weights * base_output;
    	}
    	return total_output;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
