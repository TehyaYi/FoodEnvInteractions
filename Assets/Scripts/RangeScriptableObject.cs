using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RangeScriptableObject", order = 1)]
public class RangeScriptableObject : ScriptableObject
{
	private string[] need_names = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};

	//using enum to create a dropdown list
	private enum Needs{ Terrain,RedLiquid,BlueLiquid,GreenLiquid,BlackLiquid,GasX,GasY,GasZ,Temperature,Lighting};
	[SerializeField] private Needs need;

    [SerializeField] private float weight;
    [SerializeField] private float[] ranges = new float[4];
    public string getName(){ return need_names[(int)need]; }
    public float getWeight(){ return weight; }
    public float[] getRanges(){ return ranges; }
}
