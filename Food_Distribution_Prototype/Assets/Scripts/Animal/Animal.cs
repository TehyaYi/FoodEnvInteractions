using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    public GameObject gameObject;
    public float AvailableFood;
    public readonly string Name;
    public readonly int Dominance;

    protected Animal(string name, int dominance)
    {
        this.Name = name;
        this.Dominance = dominance;
    }
}
