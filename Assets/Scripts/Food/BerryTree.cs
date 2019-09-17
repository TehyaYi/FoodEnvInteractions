using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryTree : FoodSource
{
    public static readonly string name = "Berry_Tree";
    public static readonly int baseOutput = 20;
    private readonly NeedType _foodSourceType = NeedType.Berry_Tree;

    public BerryTree(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
