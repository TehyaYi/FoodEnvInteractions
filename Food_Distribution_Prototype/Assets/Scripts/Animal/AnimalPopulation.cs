﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AnimalPopulation
{
    public readonly string AnimalName;
    public readonly int AnimalDominance;
    public readonly HashSet<FoodSourceType> AnimalEdibleFoodSourceTypes;

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

    public abstract void AddAnimal(GameObject animal);

    protected AnimalPopulation(string animalName, int animalDominance, HashSet<FoodSourceType> animalEdibleFoodSourceTypes)
    {
        this.AnimalName = animalName;
        this.AnimalDominance = animalDominance;
        this.AnimalEdibleFoodSourceTypes = animalEdibleFoodSourceTypes;
    }

    public bool IsEdible(FoodSource foodSource)
    {
        foreach(FoodSourceType edibleFoodSourceType in AnimalEdibleFoodSourceTypes)
        {
            if (edibleFoodSourceType == foodSource.Type)
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
