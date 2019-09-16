using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animal populations are holders of animals that also contain information about the animals.
abstract public class AnimalPopulation
{
    public abstract string AnimalName { get; }
    public abstract int AnimalDominance { get; }
    public abstract List<Need> Needs { get; }

    public abstract List<Animal> Animals { get; }
    public int PopulationSize { get { return Animals.Count; } }
    private float _foodPerIndividual;
    public float FoodPerIndividual { get { return _foodPerIndividual; }
      set
      {
            _foodPerIndividual = value;
            foreach(Animal animal in Animals)
            {
                animal.AvailableFood = value;
            }
      }
    }

    protected AnimalPopulation() { }

    public abstract void AddAnimal(GameObject animal);
    
    public bool IsEdible(FoodSource foodSource)
    {
        foreach(Need need in Needs)
        {
            Debug.Log("need.Name: " + need.Name + " foodSource.Name: " + foodSource.Name);
            if (need.Name == foodSource.Name)
            {
                return true;
            }
        }
        return false;
    }

    public int PopulationDominance()
    {
        return AnimalDominance * PopulationSize;
    }

    public static AnimalPopulation BuildAnimalPopulation(string animalName)
    {
        if(animalName == "Madle")
        {
            return new MadlePopulation();
        }
        else if (animalName == "Strot")
        {
            return new StrotPopulation();
        }
        else
        {
            throw new NotSupportedException(animalName + " species is not supported");
        }
    }
}
