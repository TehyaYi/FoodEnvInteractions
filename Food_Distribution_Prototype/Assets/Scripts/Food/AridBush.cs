using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AridBush : FoodSource
{
    public static readonly string name = "Arid_Bush";
    public static readonly int baseOutput = 5;
    private readonly NeedType _foodSourceType = NeedType.Arid_Bush;

    public AridBush(Vector2 position, float output) : base(output) { }

    public override NeedType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}