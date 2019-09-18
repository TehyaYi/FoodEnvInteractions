using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private Vector2 SpawnMinSet;
    public static Vector2 SpawnMin;
    [SerializeField]
    private Vector2 SpawnMaxSet;
    public static Vector2 SpawnMax;
    [SerializeField]
    private bool _displayAnimalStats;
    private List<Animal> animals = new List<Animal>();
    private List<AnimalPopulation> animalPopulations = new List<AnimalPopulation>();

    private void Awake()
    {
        SpawnMin = SpawnMinSet;
        SpawnMax = SpawnMaxSet;
    }

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
                newAnimalPopulation.AddAnimalFromGameObject(animalGameObject);
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
        SpawnMin = SpawnMinSet;
        SpawnMax = SpawnMaxSet;

        if (_displayAnimalStats)
        {
            UpdateAnimalStats();
        }
        else
        {
            GameObject[] stats = GameObject.FindGameObjectsWithTag("AnimalStats");
            foreach (GameObject stat in stats)
            {
                stat.GetComponent<Text>().text = "";
            }
        }

        Debug.DrawLine(SpawnMin, SpawnMax);
    }

    private bool AddToExistingAnimalPopulation(GameObject animal)
    {
        foreach (AnimalPopulation animalPopulation in animalPopulations)
        {
            if (animal.tag == animalPopulation.AnimalName)
            {
                animalPopulation.AddAnimalFromGameObject(animal);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns the list of animal populations.
    /// </summary>
    /// <returns></returns>
    public List<AnimalPopulation> GetAnimalPopulations()
    {
        return animalPopulations;
    }

    public static GameObject CreateAnimal(string animalTag, Vector2 position)
    {
        if (animalTag == "Madle")
        {
            return Instantiate(Resources.Load("Prefabs/Madle"), position, Quaternion.identity) as GameObject;
        }
        else if (animalTag == "Strot")
        {
            return Instantiate(Resources.Load("Prefabs/Strot"), position, Quaternion.identity) as GameObject;
        }
        else throw new System.Exception("Invalid animalTag");
    }

    private void UpdateAnimalStats()
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
}
