using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthDeclineScript : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField]
    private float FormulaTimeSpeed;

    private AnimalController _animalController;
    private FoodDistributionScript _foodDistributionScript;
    private List<AnimalPopulation> _populations;

    // Start is called before the first frame update
    void Start()
    {
        _animalController = GetComponent<AnimalController>();
        _foodDistributionScript = GetComponent<FoodDistributionScript>();
        _populations = _animalController.GetAnimalPopulations();

        InitializeGrowthTimers();
    }

    // Update is called once per frame
    void Update()
    {
        List<AnimalPopulation> extinctPopulations = new List<AnimalPopulation>();
        foreach(AnimalPopulation animalPopulation in _populations)
        {
            int numBad = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Bad);
            int numNeutral = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Neutral);
            int numGood = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Good);
            animalPopulation.GrowthTimer -= Time.deltaTime;
            //Debug.Log(animalPopulation.GrowthTimer);

            if (animalPopulation.PopulationSize <= 0)
            {
                extinctPopulations.Add(animalPopulation);
                continue;
            }
            if (animalPopulation.GrowthTimer <= 0)
            {
                if(numBad > 0)
                {
                    Animal deadAnimal = animalPopulation.RemoveRandomAnimal();
                    Destroy(deadAnimal.gameObject);
                    Debug.Log(animalPopulation.AnimalName + " died");
                }
                else if(numGood > 0)
                {
                    Vector2 position = new Vector2(Random.Range(AnimalController.SpawnMin.x, AnimalController.SpawnMax.x), Random.Range(AnimalController.SpawnMin.y, AnimalController.SpawnMax.y));
                    animalPopulation.AddAnimalFromGameObject(AnimalController.CreateAnimal(animalPopulation.AnimalName, position));
                    Debug.Log(animalPopulation.AnimalName + " born");
                }
            }
        }

        foreach(AnimalPopulation animalPopulation in extinctPopulations)
        {
            _populations.Remove(animalPopulation);
        }
        _foodDistributionScript.UpdateFoodNeeds();
        foreach (AnimalPopulation animalPopulation in _populations)
        {
            int numBad = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Bad);
            int numNeutral = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Neutral);
            int numGood = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Good);
            if (animalPopulation.GrowthTimer <= 0 && (numBad > 0 || numGood > 0))
            {
                animalPopulation.GrowthTimer = GetNewGrowthTimeInterval(animalPopulation);
            }
        }
    }

    private void InitializeGrowthTimers()
    {

        foreach(AnimalPopulation animalPopulation in _populations)
        {
            int numBad = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Bad);
            int numNeutral = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Neutral);
            int numGood = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Good);
            if (animalPopulation.GrowthTimer <= 0 && (numBad > 0 || numGood > 0))
            {
                animalPopulation.GrowthTimer = GetNewGrowthTimeInterval(animalPopulation);
            }
        }
    }

    private float GetNewGrowthTimeInterval(AnimalPopulation animalPopulation)
    {
        int numBad = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Bad);
        int numNeutral = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Neutral);
        int numGood = GetNumOfCondition(animalPopulation.Needs, NeedCondition.Good);
        if (numBad > 0)
        {
            float interval = DeclineFunction(animalPopulation.PopulationGoal, numBad, numNeutral, numGood);
            return interval;
        }
        else
        {
            float interval = GrowthFunction(animalPopulation.PopulationGoal, numBad, numNeutral, numGood);
            return interval;
        }
    }
    private float DeclineFunction(int targetSize, int numBad, int numNeutral, int numGood)
    {
        var totalNumNeeds = numBad + numNeutral + numGood;
        if (numBad <= 0) throw new System.Exception("Incorrect decline calculation");
        return (FormulaTimeSpeed * 600) / ((float)(targetSize * numBad) / (float)totalNumNeeds);
    }
    private float GrowthFunction(int targetSize, int numBad, int numNeutral, int numGood)
    {
        var totalNumNeeds = numBad + numNeutral + numGood;
        if(numGood <= 0) throw new System.Exception("Incorrect growth calculation");
        return (FormulaTimeSpeed * 600) / ((float)(targetSize * numGood) / (float)totalNumNeeds);
    }

    private int GetNumOfCondition(List<Need> needs, NeedCondition condition)
    {
        int num = 0;
        foreach(Need need in needs)
        {
            if(need.CurrentCondition == condition)
            {
                num++;
            }
        }
        return num;
    }
}
