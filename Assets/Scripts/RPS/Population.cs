using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour
{
    public Vector3Int location;//location on map
    public Tilemap map; //tilemap currently living on
    public List<TileBase> accessibleTerrain; //tiles that the pop can move through
    public List<FoodSource.FoodTypes> foodtypes; //types of food that the animal can eat
    public FoodSource food; //TODO to be removed, for testing (i.e. can this animal eat this food?)
    private Area area; //area <-> accessMap, where the pop can go

    private void Start()
    {
        location = map.WorldToCell(transform.position);
        area = new Area(ReservePartitionManager.GenerateMap(this, map));

        //Testing
        print(ReservePartitionManager.Consumes(this, food, map));
    }
    public Area GetArea() {
        return area;
    }
    public List<Vector3Int> GetMap()
    {
        return area.GetMap();
    }
}
