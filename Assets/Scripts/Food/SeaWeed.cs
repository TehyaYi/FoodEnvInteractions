using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeed : FoodSource
{
    public static readonly string name = "SeaWeed";
    public static readonly int baseOutput = 5;
    private readonly NeedType _foodSourceType = NeedType.SeaWeed;

    public SeaWeed(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
