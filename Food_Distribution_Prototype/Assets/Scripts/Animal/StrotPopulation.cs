using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrotPopulation : AnimalPopulation
{
    private List<Strot> _strots;


    public StrotPopulation() : base()
    {
        _strots = new List<Strot>();
    }

    public override string AnimalName { get { return Strot.name; } }

    public override int AnimalDominance { get { return Strot.dominance; } }

    public override List<Need> Needs { get { return needs; } }

    public override List<Animal> Animals { get { return new List<Animal>(_strots); } }


    public override void AddAnimal(GameObject animal)
    {
        Strot strot = new Strot(animal);
        _strots.Add(strot);
    }

    public static readonly List<Need> needs = new List<Need>()
    {
        new NeedF(NeedType.Fruit_Tree, "Fruit_Tree", new SortedDictionary<float, NeedCondition>()
        {
            {5f , NeedCondition.Bad},
            {10f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        }),
        new NeedF(NeedType.Arid_Bush, "Arid_Bush", new SortedDictionary<float, NeedCondition>()
        {
            {5f , NeedCondition.Bad},
            {10f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        })
    };
}
