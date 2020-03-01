using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour
{
    [SerializeField] private int id;
    public List<TileBase> accessibleTerrain; //tiles that the pop can move through
    public List<FoodSource.FoodTypes> foodtypes; //types of food that the animal can eat
    public FoodSource food; //TODO to be removed, for testing (i.e. can this animal eat this food?)
    private Area area; //area <-> accessMap, where the pop can go
    public ReservePartitionManager RPM;

    private void Start()
    {
        RPM = ReservePartitionManager.ins;

        RPM.AddPopulation(this);
        //Testing
        print(RPM.Consumes(this, food));
    }

    public Area GetArea() {
        return area;
    }

    public List<Vector3Int> GetMap()
    {
        return area.GetMap();
    }

    //for visual meme lol
    private void Update()
    {
        if (RPM.Consumes(this, food)) {
            transform.Translate((food.transform.position - transform.position) * 0.01f);
        }
    }

    public void setID(int newID) {
        id = newID;
    }

    public int getID() {
        return id;
    }
}
