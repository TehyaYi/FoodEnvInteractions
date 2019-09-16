using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMaple : FoodSource
{
    public static readonly string name = "Space_Maple";
    public static readonly int baseOutput = 25;
    private readonly FoodSourceType _foodSourceType = FoodSourceType.Space_Maple;

    public SpaceMaple(Vector2 position, float output) : base(output) { }

    public override FoodSourceType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
