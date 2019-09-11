using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrotPopulation : AnimalPopulation
{
    private List<Strot> _strots;


    public StrotPopulation() : base(Strot.name, Strot.dominance, Strot.edibleFoodSources)
    {
        _strots = new List<Strot>();
    }

    public override List<Animal> Animals { get { return new List<Animal>(_strots); } }

    public override void AddAnimal(GameObject animal)
    {
        Strot strot = new Strot(animal);
        _strots.Add(strot);
    }
}
