using System.Collections;
using System.Collections.Generic;

public class SpaceMaple : FoodSource
{
    // Why do these have to be static?
    private static readonly int _baseOutput = 25;
    private static readonly FoodSourceType _foodSourceType = FoodSourceType.Space_Maple;

    public SpaceMaple() : base(_baseOutput, _foodSourceType)
    {

    }
}
