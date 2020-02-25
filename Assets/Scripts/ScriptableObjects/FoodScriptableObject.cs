using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* A better way to store the ranges. Read the files by dragging it into food prefabs. */
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodScriptableObject", order = 1)]
public class FoodScriptableObject : ScriptableObject
{
    [Tooltip("Amount of output if all needs are moderately met.")]
    [SerializeField] private float base_output = 0;

    [Tooltip("Scriptable Objects that store details on each specific needs. Drag and drop SO into the field and values will be updated spontaneously. To remove an" +
        " SO, set it to null.")]
    //Range scriptable objects to read in from
    [SerializeField] private NeedScriptableObject[] needSO;

    [Header("Read-Only: Values read from NeedSOs")]
    [SerializeField] private string[] needs;
    [SerializeField] private float[] weights;
    private float total_weight;


    //Gets called when value of scriptable object changes in the inspector
    private void OnValidate(){
        //sorting to make the interface cleaner
        List<NeedScriptableObject> temp = new List<NeedScriptableObject>(needSO);
        temp.RemoveAll(item => item == null);
        temp.Sort();
        needSO = temp.ToArray();

        total_weight = 0;
        needs = new string[needSO.Length];
        weights = new float[needSO.Length];

        for(int i = 0; i < needSO.Length; i++){
            needs[i] = needSO[i].getName();
            weights[i] = needSO[i].getWeight();

            //negative weight will serve as harmful environment
            if (weights[i] > 0)
            {
                total_weight += weights[i];
            }
        }
    }

    public float[] getWeights(){ return weights; }
    public float getBaseOutput(){ return base_output; }
    public string[] getNeeds(){ return needs; }
    public float getTWeight() { return total_weight; }
    public NeedScriptableObject[] getNSO() { return needSO; }
}
