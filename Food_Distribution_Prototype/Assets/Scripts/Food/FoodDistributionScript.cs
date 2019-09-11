using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDistributionScript : MonoBehaviour
{
    [SerializeField]
    private FoodSourceTileMapScript foodSourceTileMapScript;
    private List<FoodSource> foodSources;
    private AnimalController animalController;

    // Start is called before the first frame update
    void Start()
    {
        animalController = GetComponent<AnimalController>();

        foodSources = foodSourceTileMapScript.getFoodSources();
        Debug.Log("Number of food sources: " + foodSources.Count);
        foreach (FoodSource foodSource in foodSources)
        {
            int totalDominance = 0;
            List<AnimalPopulation> animalsThatCanConsumeFoodSource = new List<AnimalPopulation>();
            foreach (AnimalPopulation animalPopulation in animalController.GetAnimalPopulations())
            {
                if (animalPopulation.IsEdible(foodSource))
                {
                    totalDominance += animalPopulation.PopulationDominance();
                    animalsThatCanConsumeFoodSource.Add(animalPopulation);
                }
            }
                        
            foreach(AnimalPopulation animalPopulation in animalsThatCanConsumeFoodSource)
            {
                Debug.Log("(float) animalPopulation.PopulationDominance(): " + (float)animalPopulation.PopulationDominance());
                Debug.Log("(float) totalDominance): " + (float)totalDominance);
                Debug.Log("foodSource.Output: " + foodSource.Output);
                float populationFood = ((float) animalPopulation.PopulationDominance() / (float) totalDominance) * foodSource.Output;
                float foodPerIndividual = populationFood / (float) animalPopulation.PopulationSize;
                animalPopulation.FoodPerIndividual += foodPerIndividual;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
