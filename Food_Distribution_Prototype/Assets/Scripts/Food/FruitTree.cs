using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : FoodSource
{
    public static readonly string name = "Fruit_Tree";
    public static readonly int baseOutput = 15;
    private readonly NeedType _foodSourceType = NeedType.Fruit_Tree;

    public FruitTree(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
