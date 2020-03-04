using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 This method is 'regional' because it assumes that level is divided by different regions.
 A region is an area in which all animals that are inside the region can access all of the plants inside the region. 

 This method is considered 'naive' because it does not take into the fact that populations can get 'full'.
*/

/*
public class RegionalFoodDistribution_naive : MonoBehaviour
{
   //This variable can be accessed and called by other scripts when a food source or population changes
   public bool needToDistributeFood;

    //This is the main output function of the distribution script
    //Note: not sure whether individual food is a float or an int, or if the food dist team implements this method or the needs team
    public void updateNeedRating(AnimalPopulation animalPopulation, float individualFood, NeedType foodSourceType){
        //TODO: Set the need of the population to being met as good, neutral, or bad depending on the amount of food they receive
    }

    // Get the different food source types in the region
    private List<FoodSourceType> getFoodSourceTypes(Region region)
    {
        List<FoodSourceType> foodSourceTypes = new List<FoodSourceType>();
        // TODO: get all the food source in the region by its type, `foodSourceType`.
        return foodSourceTypes;
    }

    // Get the different food sources of that type in the region
    private List<FoodSource> getFoodSources(Region region, FoodSourceType type)
    {
        List<FoodSource> foodSources = new List<FoodSource>();
        // TODO: get all the food source in the region by its type, `foodSourceType`.
        return foodSourceTypes;
    }

    private float getFoodSourceOutput(FoodSource foodSource)
    {
        float output = 0f;
        // TODO: get food source output
        return output;
    }

        // Get a list of animal population that has access to given region
    private List<AnimalPopulation> getPopulationsCanAccess(Region region)
    {
        List<AnimalPopulation> populationsCanAccess = new List<AnimalPopulation>();
        // TODO: get list of animal population that has access to the region
        return populationsCanAccess;
    }


// Get list of populations that can consume given food source in given list of population
    private List<AnimalPopulation> getPopulationsThatConsumeFoodSource(List<AnimalPopulation> allPopulations, NeedType foodSourceType)
    {
        List<AnimalPopulation> canConsumePopulations = new List<AnimalPopulation>();
        foreach(AnimalPopulation population in allPopulations)
        {
            // TODO: check in population can consume foodSourceType
            if (true)
            {
                canConsumePopulations.Add(population);
            }
        }
        return canConsumePopulations;
    }

    private float getPoplulationDomiance(AnimalPopulation population)
    {
        float domiance 0f;
        // TODO: get dominace rating for given population
        return domiance;
    }

    private float getPopulationTotalDominace(AnimalPopulation population)
    {
        float totalDomiance = getPoplulationDomiance(population) * getPopulationSize(population);
        return totalDomiance;
    }

    private float getPopulationSize(AnimalPopulation population)
    {
        float populationSize = 0f;
        // TODO: get animal population size
        return populationSize;
    }

    
    private void distributeTotalFood(FoodSourceType type, int totalFood, List<AnimalPopulation> populationsList)
    {
        float totalDomiance = 0f;
        foreach(AnimalPopulation population in populations)
        {
            totalDomiance = totalDomiance + getPopulationTotalDominace(population);
        }
        foreach(AnimalPopulation population in populations)
        {
            float groupFood = totalFood * getPopulationTotalDominace(population) / totalDomiance;
            float individualFood = groupFood / getPopulationSize();
            updateNeedRating(population, individualFood, type);
        }
    }

    private void distributeFoodByRegion(Region region)
    {
        List<FoodSourceType> listOfTypes = getFoodSourceTypes(region);
        List<AnimalPopulation> listOfAnimals = getPopulationsCanAccess(region);
        foreach(FoodSourceType type in listOfTypes)
        {
            List<FoodSource> foodSources = getFoodSources(region, type);
            int totalFood = 0;
            foreach(FoodSource source in foodSources){
                totalFood = totalFood + getFoodSourceOutput(source);
            }
            List<AnimalPopulation> animalsCanEat = getPopulationsThatConsumeFoodSource(listOfAnimals);
            distributeTotalFood(type, totalFood, animalsCanEat);
        }
    }

    private void distributeFood(List<Region> regions)
    {
        foreach(Region region in regions)
        {
            distributeFoodByRegion(region);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(needToDistributeFood){
            distributeFood();
            needToDistributeFood = false;
        }
    }
}
*/