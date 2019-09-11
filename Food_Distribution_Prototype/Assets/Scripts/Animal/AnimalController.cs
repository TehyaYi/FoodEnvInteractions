using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    private List<Animal> animals = new List<Animal>();
    private List<AnimalPopulation> animalPopulations = new List<AnimalPopulation>();
    

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> animalGameObjects = new List<GameObject>();
        animalGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Madle"));
        animalGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Strot"));
        Debug.Log("Number of animals: " + animalGameObjects.Count);
        foreach(GameObject animalGameObject in animalGameObjects)
        {
            Debug.Log(animalGameObject.transform.position);
            // If this animal is a species we don't have a population for yet
            if (!AddToExistingAnimalPopulation(animalGameObject))
            {
                Debug.Log(animalGameObject.tag);
                AnimalPopulation newAnimalPopulation = AnimalPopulation.BuildAnimalPopulation(animalGameObject.tag);
                newAnimalPopulation.AddAnimal(animalGameObject);
                animalPopulations.Add(newAnimalPopulation);
            }
        }
        foreach(AnimalPopulation population in animalPopulations)
        {
            animals.AddRange(population.Animals);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Animal animal in animals)
        {
            animal.gameObject.GetComponentInChildren<Text>().text = animal.AvailableFood.ToString();
        }
    }

    private bool AddToExistingAnimalPopulation(GameObject animal)
    {
        foreach(AnimalPopulation animalPopulation in animalPopulations)
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
