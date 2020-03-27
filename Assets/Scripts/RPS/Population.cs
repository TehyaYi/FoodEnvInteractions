using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// [Deprecated]Population class that contain essential data members for RPM and PDM
/// </summary>
public class Populationt : MonoBehaviour
{
    //Considering using the auto implementation { get; set; } but not exactly sure of the convention
    //to be cleaned up afterwards

    public int id { get; private set; } //id for RPM
    public List<TileType> accessibleTerrain; //tiles that the pop can move through

    //TODO to be removed, for testing (i.e. can this animal eat this food?)
    public FoodSource food; 

    public ReservePartitionManager RPM;


    //info below are for PopDensity

    [SerializeField] public int radius { get; } = 3;//should probably implemented in Scriptable objects
    [SerializeField] private float animalSize = 10;//e.g. 10 kg/animal
    [SerializeField] private float animalNumber = 100;//e.g. Total of 100 animals
    public int space; //number of tiles within radius that are accessible




    private void Start()
    {
        RPM = ReservePartitionManager.ins;

        //RPM.AddPopulation(this);
        //Testing
        //print(RPM.CanAccess(this, food.transform.position));
    }

    /// <summary>
    /// Set the ID of a population for Reserve Partitioning purposes.
    /// Should never be called by a class other than the Reserve Partitioning Manager!
    /// </summary>
    public void SetID(int newID) {
        id = newID;
    }

    /// <summary>
    /// Get the ID of a population assigned by Reserve Paritition Manager.
    /// </summary>
    public int GetID() {
        return id;
    }

    /// <summary>
    /// Set the number of tiles within radius that are accessible.
    /// Should only be called by PDM when constructing density map.
    /// </summary>
    public void SetSpace(int nsp)
    {
        space = nsp;
    }

    /// <summary>
    /// Get the number of tiles within radius that are accessible.
    /// </summary>
    public int GetSpace() { return space; }

    /// <summary>
    /// Get the total weight of the animals.
    /// </summary>
    public float GetWeight() { return animalSize * animalNumber; }
}
