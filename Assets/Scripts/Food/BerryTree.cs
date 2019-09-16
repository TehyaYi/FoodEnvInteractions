using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryTree : FoodSource
{
    public static readonly string name = "Berry_Tree";
    public static readonly int baseOutput = 20;
    private readonly FoodSourceType _foodSourceType = FoodSourceType.Berry_Tree;

    public BerryTree(Vector2 position, float output) : base(output) { }

    public override FoodSourceType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
