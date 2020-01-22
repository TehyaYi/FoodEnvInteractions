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
