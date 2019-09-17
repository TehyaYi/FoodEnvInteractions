using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Distributes the food provided by the food sources to the animals that can eat them.
public class FoodDistributionScript : MonoBehaviour
{
    [SerializeField]
    private FoodSourceTileMapScript foodSourceTileMapScript;
    private List<FoodSource> foodSources;
    private AnimalController animalController;

    void Start()
    {
        animalController = GetComponent<AnimalController>();

        // Get the list of food sources in the reserve.
        foodSources = foodSourceTileMapScript.getFoodSources();
        Debug.Log("Number of food sources: " + foodSources.Count);
        foreach (FoodSource foodSource in foodSources)
        {
            int totalDominance = 0;

            // The list of animals that can consume the food source.
            List<AnimalPopulation> animalsThatCanConsumeFoodSource = new List<AnimalPopulation>();

            // Iterate over all the existing animal populations and see if the food source is edible to any of them and
            // add them to animalsThatCanConsumeFoodSource list and also count up the total amount of dominance that will
            // be competing for the food source.
            foreach (AnimalPopulation animalPopulation in animalController.GetAnimalPopulations())
            {
                if (animalPopulation.IsEdible(foodSource))
                {
                    totalDominance += animalPopulation.PopulationDominance();
                    animalsThatCanConsumeFoodSource.Add(animalPopulation);
                }
            }
            // Calculate the FoodPerIndividual score for each animal population.
            foreach (AnimalPopulation animalPopulation in animalsThatCanConsumeFoodSource)
            {
                float populationFood = ((float)animalPopulation.PopulationDominance() / (float)totalDominance) * foodSource.Output;
                float foodPerIndividual = populationFood / (float)animalPopulation.PopulationSize;
                Need<float> foodSourceNeed = (Need<float>)animalPopulation.GetNeed(foodSource.Type);
                foodSourceNeed.CurrentValue += foodPerIndividual;
            }
        }
    }
}
