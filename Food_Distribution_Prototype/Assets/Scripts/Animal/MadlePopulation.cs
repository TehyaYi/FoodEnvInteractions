using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadlePopulation : AnimalPopulation
{
    private List<Strot> _madles;


    public MadlePopulation() : base(Madle.name, Madle.dominance, Madle.needs )
    {
        _madles = new List<Strot>();
    }

    public override List<Animal> Animals { get { return new List<Animal>(_madles); } }

    public override void AddAnimal(GameObject animal)
    {
        Strot madle = new Strot(animal);
        _madles.Add(madle);
    }
}
