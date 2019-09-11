using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strot : Animal
{
    public static readonly string name = "Strot";
    public static readonly int dominance = 2;

    public Strot(GameObject animal) : base(name, dominance, needs)
    {
        gameObject = animal;
    }

    public static readonly List<Need> needs = new List<Need>()
    {
        new NeedF("Fruit_Tree", new SortedDictionary<float, NeedCondition>()
        {
            {5f , NeedCondition.Bad},
            {10f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        })
    };
}
