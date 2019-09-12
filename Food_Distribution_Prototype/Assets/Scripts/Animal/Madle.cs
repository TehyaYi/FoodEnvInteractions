﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madle : Animal
{
    public static readonly string name = "Madle";
    public static readonly int dominance = 5;

    public Madle(GameObject animal) : base(name, dominance, needs)
    {
        gameObject = animal;
    }


    public static readonly List<Need> needs = new List<Need>()
    {
        new NeedF("Space_Maple", new SortedDictionary<float, NeedCondition>()
        {
            {10f , NeedCondition.Bad},
            {20f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        }),
        new NeedF("Fruit_Tree", new SortedDictionary<float, NeedCondition>()
        {
            {5f , NeedCondition.Bad},
            {10f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        })
    };
}