using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : FoodSource
{
    public static readonly string name = "Berry_Bush";
    public static readonly int baseOutput = 5;
    private readonly NeedType _foodSourceType = NeedType.Berry_Bush;

    public BerryBush(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
