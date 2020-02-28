using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour
{
    [SerializeField] private int id;
    public Vector3Int location;//location on map
    public Tilemap map; //tilemap currently living on
    public List<TileBase> accessibleTerrain; //tiles that the pop can move through
    public List<FoodSource.FoodTypes> foodtypes; //types of food that the animal can eat
    public FoodSource food; //TODO to be removed, for testing (i.e. can this animal eat this food?)
    private Area area; //area <-> accessMap, where the pop can go
    public ReservePartitionManager RPM;

    private void Start()
    {
        RPM = ReservePartitionManager.ins;
        location = map.WorldToCell(transform.position);
        area = new Area(RPM.GenerateMap(this, map));

        //Testing
        print(RPM.Consumes(this, food, map));
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
        if (RPM.Consumes(this, food, map)) {
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
