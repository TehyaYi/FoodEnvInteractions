using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafyBush : FoodSource
{
    public static readonly string name = "Leafy_Bush";
    public static readonly int baseOutput = 10;
    private readonly NeedType _foodSourceType = NeedType.Leafy_Bush;

    public LeafyBush(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
