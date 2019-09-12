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

    public override List<Need> Needs { get { return Strot.needs; } }

    public override List<Animal> Animals { get { return new List<Animal>(_strots); } }


    public override void AddAnimal(GameObject animal)
    {
        Strot strot = new Strot(animal);
        _strots.Add(strot);
    }
}
