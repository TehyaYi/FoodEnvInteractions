using System.Collections;
using System.Collections.Generic;

public class AridBush : FoodSource
{
    public static readonly string name = "Arid_Bush";
    // Why do these have to be static?
    private static readonly int _baseOutput = 5;
    private static readonly FoodSourceType _foodSourceType = FoodSourceType.Arid_Bush;

    public AridBush() : base(name, _baseOutput, _foodSourceType)
    {

    }
}