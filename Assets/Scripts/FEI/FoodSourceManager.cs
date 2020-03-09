using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSourceManager : MonoBehaviour
{
    //Food source manager will be keeping references to the food sources and telling the food dis. sys 
    //and environmental interactions sys. to update when they need to

    //dictionary(?) of all active food source instances
    private IDictionary<int, FoodSource> foodSourceDict = new Dictionary<int, FoodSource>();
    private int currIndex= 0;

    //Should we have instantiation functions for different food sources? and how to organize it all --maybe a table

    public int add(FoodSource newFoodSource) {
        currIndex++;
        foodSourceDict.Add(currIndex, newFoodSource);
        // TODO : tell food dist and food env to update
        return currIndex;
    }
    
    public void delete(int currIndex)
    {
        foodSourceDict.Remove(currIndex);
        // TODO : tell food dist and food env to update
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
