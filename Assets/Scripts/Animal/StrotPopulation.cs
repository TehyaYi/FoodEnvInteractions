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

    private readonly int _populationGoal = 50;
    public override int PopulationGoal { get { return _populationGoal; } }

    public override void AddAnimalFromGameObject(GameObject animal)
    {
        Strot strot = new Strot(animal);
        _strots.Add(strot);
    }

    public override Animal RemoveRandomAnimal()
    {
        if (_strots.Count == 0) return null;
        int randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0, _strots.Count));
        Animal removedAnimal = _strots[randomIndex];
        _strots.RemoveAt(randomIndex);
        return removedAnimal;
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
