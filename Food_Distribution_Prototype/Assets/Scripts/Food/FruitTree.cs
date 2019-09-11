using System.Collections;
using System.Collections.Generic;

public class FruitTree : FoodSource
{
    public static readonly string name = "Fruit_Tree";
    // Why do these have to be static?
    private static readonly int _baseOutput = 15;
    private static readonly FoodSourceType _foodSourceType = FoodSourceType.Fruit_Tree;

    public FruitTree() : base(name, _baseOutput, _foodSourceType)
    {

    }
}
