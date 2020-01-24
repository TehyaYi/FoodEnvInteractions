using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* A better way to store the ranges. Read the files by dragging it into food prefabs. */
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodScriptableObject", order = 1)]
public class FoodScriptableObject : ScriptableObject
{
    [SerializeField] private float base_output = 0;
    
    public string[] needs = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};
    private const int numOfNeeds = 10;

	[SerializeField] private float[] weights = new float[numOfNeeds];
    private float total_weight;

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
    
    public float[][] getRanges(){
        ranges = new float[][]{     terrain_ranges, 
                                    red_liquid_ranges, 
                                    blue_liquid_ranges,
                                    black_liquid_ranges,
                                    gasX_ranges,
                                    gasY_ranges,
                                    gasZ_ranges, 
                                    temperature_ranges, 
                                    lighting_ranges     };
        return ranges;
    }
    public float[] getWeights(){ return weights; }
    public float getBaseOutput(){ return base_output; }
    public string[] getNeeds(){ return needs; }
}
