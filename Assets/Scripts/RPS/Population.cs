using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour
{
    [SerializeField] private int id; //id for RPM
    public List<TileType> accessibleTerrain; //tiles that the pop can move through

    //TODO should be handled by other systems instead, just for testing
    public List<FoodSource.FoodTypes> foodtypes; //types of food that the animal can eat

    //TODO to be removed, for testing (i.e. can this animal eat this food?)
    public FoodSource food; 

    public ReservePartitionManager RPM;


    //info below are for PopDensity

    [SerializeField] public int radius = 3;//should probably implemented in Scriptable files
    private int space;
    [SerializeField] private float animalSize = 10;//e.g. 10 kg/animal
    [SerializeField] private float animalNumber = 100;//e.g. Total of 100 animals




    private void Start()
    {
        RPM = ReservePartitionManager.ins;

        RPM.AddPopulation(this);
        //Testing
        print(RPM.CanAccess(this, food.transform.position));
    }

    /// <summary>
    /// Set the ID of a population for Reserve Partitioning purposes.
    /// Should never be called by a class other than the Reserve Partitioning Manager!
    /// </summary>
    public void setID(int newID) {
        id = newID;
    }

    public int getID() {
        return id;
    }

    public int getRadius() { return radius; }

    public void setSpace(int nsp)
    {
        space = nsp;
    }

    public int getSpace() { return space; }

    //size*number = 1000 kg
    public float getPopSize() { return animalSize * animalNumber; }
}
