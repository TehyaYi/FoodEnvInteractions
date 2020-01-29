using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RangeScriptableObject", order = 1)]
public class RangeScriptableObject : ScriptableObject
{
	//passing string to make it more readable in code
	private string[] need_names = {"Terrain", "Red Liquid", "Blue Liquid", "Green Liquid","Black Liquid","Gas X","Gas Y","Gas Z","Temperature","Lighting"};

	//using enum to create a dropdown list
	private enum Needs{ Terrain,RedLiquid,BlueLiquid,GreenLiquid,BlackLiquid,GasX,GasY,GasZ,Temperature,Lighting};
	[SerializeField] private Needs need;

    [SerializeField] private float weight;
    [SerializeField] private float[] ranges = new float[4];
    
	//passing string to make it more readable in code
    public string getName(){ return need_names[(int)need]; }
    public float getWeight(){ return weight; }
    public float[] getRanges(){ return ranges; }
}
