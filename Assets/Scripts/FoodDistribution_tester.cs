using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 This method assumes 'one region' i.e. all animals and plants present on the map can access everything
*/


public class FoodDistribution_tester : MonoBehaviour
{
 
    public void distributeFood()
    {
        List<FoodSource> foodSources = getAllFoodOnMap();
       //List<AnimalPopulation> listOfAnimals = getAllPopulations();
    }

/*
    // Get the different food source types available on the map
    private List<FoodTypes> getAllFoodTypesAvailable()
    {
        List<FoodTypes> foodSourceTypes = new List<FoodTypes>();
        // TODO: get all the food source in the region by its type, `foodSourceType`.
        return foodSourceTypes;
    }
*/

    // Get the different food sources of that type in the region
    private List<FoodSource> getAllFoodOnMap()
    {
        List<FoodSource> foodSources = new List<FoodSource>();
        // TODO: get all food sources on the map
        return foodSources;
    }

    private float getFoodSourceOutput(FoodSource foodSource)
    {
        float output = 0f;
        // TODO: get food source output
        return output;
    }
    

/*
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
*/

    /*
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
*/



}
