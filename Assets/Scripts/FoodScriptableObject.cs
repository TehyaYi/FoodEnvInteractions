using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* A better way to store the ranges. Read the files by dragging it into food prefabs. */
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodScriptableObject", order = 1)]
public class FoodScriptableObject : ScriptableObject
{
    [SerializeField] private float base_output = 0;
    [SerializeField] private string[] needs;
    [SerializeField] private float[] weights;
    private float total_weight;

    //Range scriptable objects to read in from
    [SerializeField] private RangeScriptableObject[] rangeSO;

    //representing the ranges as a matrix (table).
    private float[][] ranges;

    private bool initialized = false;

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
            // initialized = true; //Need to make it false when simulation ends
        }
    }

    public float[][] getRanges(){ return ranges; }
    public float[] getWeights(){ return weights; }
    public float getBaseOutput(){ return base_output; }
    public string[] getNeeds(){ return needs; }
    public RangeScriptableObject[] getRSO() { return rangeSO; }
}
