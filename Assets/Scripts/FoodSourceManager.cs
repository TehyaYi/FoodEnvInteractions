
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class FoodSourceManager : MonoBehaviour
{
    //Food source manager will be keeping references to the food sources and telling the food dis. sys 
    //and environmental interactions sys. when they need to update

    //dictionary(?) of all active food source instances
    private IDictionary<int, FoodSource> foodSourceDict = new Dictionary<int, FoodSource>();
    private int currIndex = 0;

    //private RealisticFoodDistributionSystem foodDis = new RealisticFoodDistributionSystem();
    //private RegionalFoodDistribution_naive regFoodDist = new RegionalFoodDistribution_naive();


    // This is a refference of all the food sources objects that will be given when the manager is initiate
    public List<FoodSource> allFoodSources;
    private GameObject[] foodSources;



    private List<FoodSource> getAllFoodSourceByType(string type)
    {
        List<FoodSource> foodSourcesByType = new List<FoodSource>();

        foreach (FoodSource foodSource in this.allFoodSources)
        {
            if (foodSource.getFoodType() == type)
            {
                foodSourcesByType.Add(foodSource);
            }
        }

        return foodSourcesByType;
    }


    public int add(FoodSource newFoodSource)
    {
        currIndex++;
        foodSourceDict.Add(currIndex, newFoodSource);

        // TODO : tell food dist and food env to update
        updateFoodSource(newFoodSource);

        return currIndex;
    }

    public void delete(int index)
    {
        foodSourceDict.Remove(index);

        // TODO : tell food dist and food env to update
        updateFoodSource(foodSourceDict[index]);
    }

    private void updateFoodSource(FoodSource foodSource)
    {
        List<FoodSource> foodSourcesToDistribute = getAllFoodSourceByType(foodSource.getFoodType());
        this.foodSources = GameObject.FindGameObjectsWithTag("foodSource");

        //this.foodDis.update(foodSourcesToDistribute);
        //this.regFoodDist.needToDistributeFood = true;
        //this.update = true;
        distributeFood(foodSourcesToDistribute);
    }
} 

*/