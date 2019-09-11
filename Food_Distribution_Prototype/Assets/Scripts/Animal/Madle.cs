using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madle : Animal
{
    public static readonly string name = "Madle";
    public static readonly int dominance = 5;
    public static readonly HashSet<FoodSourceType> edibleFoodSources = new HashSet<FoodSourceType> { FoodSourceType.Space_Maple, FoodSourceType.Fruit_Tree };

    public Madle(GameObject animal) : base(name, dominance, edibleFoodSources)
    {
        gameObject = animal;
    }

}
