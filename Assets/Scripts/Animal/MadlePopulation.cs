using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadlePopulation : AnimalPopulation
{
    private List<Madle> _madles;


    public MadlePopulation()
    {
        _madles = new List<Madle>();
    }

    public override string AnimalName { get { return Madle.name; } }

    public override int AnimalDominance { get { return Madle.dominance; } }

    public override List<Need> Needs { get { return needs; } }

    public override List<Animal> Animals { get { return new List<Animal>(_madles); }}

    private readonly int _populationGoal = 15;
    public override int PopulationGoal { get { return _populationGoal; } }

    public override void AddAnimalFromGameObject(GameObject animal)
    {
        Madle madle = new Madle(animal);
        _madles.Add(madle);
    }

    public override Animal RemoveRandomAnimal()
    {
        if (_madles.Count == 0) return null;
        int randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0, _madles.Count));
        Animal removedAnimal = _madles[randomIndex];
        _madles.RemoveAt(randomIndex);
        return removedAnimal;
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
        }),
        new NeedF(NeedType.Leaf_Tree, "Leaf_Tree", new SortedDictionary<float, NeedCondition>()
        {
            {30f , NeedCondition.Bad},
            {50f, NeedCondition.Neutral},
            {float.MaxValue, NeedCondition.Good}
        })
    };
}
