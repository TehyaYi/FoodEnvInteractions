using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    private List<Animal> animals = new List<Animal>();
    private List<AnimalPopulation> animalPopulations = new List<AnimalPopulation>();

    void Start()
    {
        List<GameObject> animalGameObjects = new List<GameObject>();
        animalGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Madle"));
        animalGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Strot"));
        Debug.Log("Number of animals: " + animalGameObjects.Count);
        foreach (GameObject animalGameObject in animalGameObjects)
        {
            // If this animal is a species we don't have a population for yet
            if (!AddToExistingAnimalPopulation(animalGameObject))
            {
                AnimalPopulation newAnimalPopulation = AnimalPopulation.BuildAnimalPopulation(animalGameObject.tag);
                newAnimalPopulation.AddAnimal(animalGameObject);
                animalPopulations.Add(newAnimalPopulation);
            }
        }
        foreach (AnimalPopulation population in animalPopulations)
        {
            animals.AddRange(population.Animals);
        }
    }

    void Update()
    {
        foreach (AnimalPopulation animalPopulation in animalPopulations)
        {
            foreach (Animal animal in animalPopulation.Animals)
            {
                string text = "";
                foreach (Need need in animalPopulation.Needs)
                {
                    string needText = need.Name + ": " + need.CurrentCondition + ", " + ((Need<float>)need).CurrentValue;
                    text += needText + "\n";
                }
                animal.gameObject.GetComponentInChildren<Text>().text = text;
            }
        }
    }

    private bool AddToExistingAnimalPopulation(GameObject animal)
    {
        foreach (AnimalPopulation animalPopulation in animalPopulations)
        {
            if (animal.tag == animalPopulation.AnimalName)
            {
                animalPopulation.AddAnimal(animal);
                return true;
            }
        }
        return false;
    }

    public List<AnimalPopulation> GetAnimalPopulations()
    {
        return animalPopulations;
    }
}
