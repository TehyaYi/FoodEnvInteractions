using System.Collections;
using System.Collections.Generic;

public class FruitTree : FoodSource
{
    // Why do these have to be static?
    private static readonly int _baseOutput = 15;
    private static readonly FoodSourceType _foodSourceType = FoodSourceType.Fruit_Tree;

    public FruitTree() : base(_baseOutput, _foodSourceType)
    {

    }
}
