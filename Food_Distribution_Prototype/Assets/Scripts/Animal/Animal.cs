using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    public GameObject gameObject;
    public float AvailableFood;
    public readonly string Name;
    public readonly int Dominance;
    public readonly HashSet<FoodSourceType> EdibleFoodSources;

    protected Animal(string name, int dominance, HashSet<FoodSourceType> edibleFoodSources)
    {
        this.Name = name;
        this.Dominance = dominance;
        this.EdibleFoodSources = edibleFoodSources;
    }

}
