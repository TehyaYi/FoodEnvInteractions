using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* A better way to store the ranges. Read the files by dragging it into food prefabs. */
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodScriptableObject", order = 1)]
public class FoodScriptableObject : ScriptableObject
{
    [SerializeField] private float base_output = 0;
    
    public string[] needs = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};

	[SerializeField] private float[] weights;
    private float total_weight;

    //Range scriptable objects to read in from
    [SerializeField] private RangeScriptableObject[] rangeSO;

    //representing the ranges as a matrix (table).
    private float[][] ranges;

    private bool initialized = false;

/*
    //variables for the table, determines what values are good, moderate, or bad for the plant
    //[0] = low bound for good, [1] = high bound for good, [2] = low bound for mod, [3] = high bound for mod, otherwise bad.
    [SerializeField] private float[] terrain_ranges = new float[4];
    [SerializeField] private float[] red_liquid_ranges = new float[4];
    [SerializeField] private float[] blue_liquid_ranges = new float[4];
    [SerializeField] private float[] green_liquid_ranges = new float[4];
    [SerializeField] private float[] black_liquid_ranges = new float[4];
    [SerializeField] private float[] gasX_ranges = new float[4];
    [SerializeField] private float[] gasY_ranges = new float[4];
    [SerializeField] private float[] gasZ_ranges = new float[4];
    [SerializeField] private float[] temperature_ranges = new float[4];
    [SerializeField] private float[] lighting_ranges = new float[4];
*/

    public void init(){
        if(!initialized){
            needs = new string[rangeSO.Length];
            weights = new float[rangeSO.Length];
            ranges = new float[rangeSO.Length][];
            for(int i = 0; i < ranges.Length; i++){
                needs[i] = rangeSO[i].getName();
                weights[i] = rangeSO[i].getWeight();
                ranges[i] = rangeSO[i].getRanges();
            }
            //initialized = true; //Need to make it false when simulation ends
        }
    }

    public float[][] getRanges(){
        /*
        ranges = new float[][]{     terrain_ranges, 
                                    red_liquid_ranges, 
                                    blue_liquid_ranges,
                                    black_liquid_ranges,
                                    gasX_ranges,
                                    gasY_ranges,
                                    gasZ_ranges, 
                                    temperature_ranges, 
                                    lighting_ranges     };
        */
        return ranges;
    }
    public float[] getWeights(){ return weights; }
    public float getBaseOutput(){ return base_output; }
    public string[] getNeeds(){ return needs; }
}
