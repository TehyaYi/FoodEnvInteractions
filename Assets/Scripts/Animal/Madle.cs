using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madle : Animal
{
    public static readonly string name = "Madle";
    public static readonly int dominance = 5;

    public Madle(GameObject animal) : base(name, dominance)
    {
        gameObject = animal;
    }
}