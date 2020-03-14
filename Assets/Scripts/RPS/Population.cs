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

    public int space; //amount of living space

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

    public void setSpace(int nsp)
    {
        space = nsp;
    }

    public int getSpace() { return space; }
}
