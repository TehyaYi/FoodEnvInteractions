﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadlePopulation : AnimalPopulation
{
    private List<Strot> _madles;


    public MadlePopulation()
    {
        _madles = new List<Strot>();
    }

    public override string AnimalName { get { return Madle.name; } }

    public override int AnimalDominance { get { return Madle.dominance; } }

    public override List<Need> Needs { get { return needs; } }

    public override List<Animal> Animals { get { return new List<Animal>(_madles); } }


    public override void AddAnimal(GameObject animal)
    {
        Strot madle = new Strot(animal);
        _madles.Add(madle);
    }

    public static readonly List<Need> needs = new List<Need>()
    {
        new NeedF(NeedType.Space_Maple, "Space_Maple", new SortedDictionary<float, NeedCondition>()
        {
            {10f , NeedCondition.Bad},
            {20f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        }),
        new NeedF(NeedType.Fruit_Tree, "Fruit_Tree", new SortedDictionary<float, NeedCondition>()
        {
            {5f , NeedCondition.Bad},
            {10f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        })
    };
}
