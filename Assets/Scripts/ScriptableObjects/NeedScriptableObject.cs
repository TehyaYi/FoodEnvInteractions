using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NeedScriptableObject", order = 1)]
public class NeedScriptableObject : ScriptableObject, IComparable<NeedScriptableObject>
{
	//passing string to make it more readable in code
	private string[] need_names = {"Terrain", "Liquid", "Gas X","Gas Y","Gas Z","Temperature"};
    
	//using enum to create a dropdown list
	public enum Needs{ Terrain,Liquid,GasX,GasY,GasZ,Temperature};
	[SerializeField] private Needs need = Needs.GasX;

	[Tooltip("Weight when determining output")]
	[SerializeField] private float weight = 0;

    [Tooltip("[Good Low, Good High, Mod Low, Mod High]")]
    [SerializeField] private float[] ranges = new float[4];
    
	//passing string to make it more readable in code
    public string getName(){ return need_names[(int)need]; }

    public float getWeight(){ return weight; }

    public float[] getRanges(){ return ranges; }

    public Needs getNeed() { return need; }


	//Calculate how good the need are met and return as int condition
	//0 = bad, 1 = moderate, 2 = good
	//values = current value of the need
	public int calculateCondition(float val) {
		int condition;
		if (val >= ranges[0] && val <= ranges[1] &&
			   !(ranges[0] == 0 && ranges[1] == 0))
		{//upper and lower being 0 means no value satisfies
			condition = 2;
		}
		else if (val >= ranges[2] && val <= ranges[3] &&
		   !(ranges[2] == 0 && ranges[3] == 0))
		{//upper and lower being 0 means no value satisfies
			condition = 1;
		}
		else
		{
			condition = 0;
		}
		return condition;
	}

    //to be sorted in FSO
	public int CompareTo(NeedScriptableObject other) {
		if (need < other.need)
		{
			return -1;
		}
		else if (need > other.need)
		{
			return 1;
		}
		else { //need == other.need
			if (weight < other.weight)
			{
				return -1;
			}
			else if (weight > other.weight)
			{
				return 1;
			}
			else //need and weight are both equal
			{
				return 0;
			}
        }
    }
}
