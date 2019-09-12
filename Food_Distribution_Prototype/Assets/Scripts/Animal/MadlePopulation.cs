using System.Collections;
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

    public override List<Need> Needs { get { return Madle.needs; } }

    public override List<Animal> Animals { get { return new List<Animal>(_madles); } }


    public override void AddAnimal(GameObject animal)
    {
        Strot madle = new Strot(animal);
        _madles.Add(madle);
    }
}
