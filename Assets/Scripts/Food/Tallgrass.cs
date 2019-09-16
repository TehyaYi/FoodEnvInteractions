﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tallgrass : FoodSource
{
    public static readonly string name = "Tallgrass";
    public static readonly int baseOutput = 5;
    private readonly FoodSourceType _foodSourceType = FoodSourceType.Tallgrass;

    public Tallgrass(Vector2 position, float output) : base(output) { }

    public override FoodSourceType Type { get { return _foodSourceType; } }

    public override string Name { get { return name; } }

    public override int BaseOutput { get { return baseOutput; } }
}
