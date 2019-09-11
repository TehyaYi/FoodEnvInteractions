using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    public GameObject gameObject;
    public float AvailableFood;
    public readonly string Name;
    public readonly int Dominance;
    public readonly List<Need> Needs;

    protected Animal(string name, int dominance, List<Need> needs)
    {
        this.Name = name;
        this.Dominance = dominance;
        this.Needs = needs;
    }
}
