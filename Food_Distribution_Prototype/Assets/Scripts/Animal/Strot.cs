using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strot : Animal
{
    public static readonly string name = "Strot";
    public static readonly int dominance = 2;
    public static readonly HashSet<FoodSourceType> edibleFoodSources = new HashSet<FoodSourceType> { FoodSourceType.Fruit_Tree };

    public Strot(GameObject animal) : base(name, dominance, edibleFoodSources)
    {
        gameObject = animal;
    }

}
