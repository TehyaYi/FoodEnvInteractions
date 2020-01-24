using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RangeScriptableObject", order = 1)]
public class RangeScriptableObject : ScriptableObject
{
	[SerializeField] private string need_name;
    [SerializeField] private float weight;
    [SerializeField] private float[] ranges = new float[4];
    public string getName(){ return need_name; }
    public float getWeight(){ return weight; }
    public float[] getRanges(){ return ranges; }
}
