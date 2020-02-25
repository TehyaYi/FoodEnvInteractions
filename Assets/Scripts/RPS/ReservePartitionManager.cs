using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReservePartitionManager : MonoBehaviour
{
    public int[,] PopulateAccessMap(Population pop, Tilemap tilemap) {
        List<Vector3Int> queue = new List<Vector3Int>();
        List<Vector3Int> closed = new List<Vector3Int>();

        queue.Add(pop.location);
        while (queue.Count > 0) {

        }
        return null;

    }

    public bool Consumes(Population pop, FoodSource food, Tilemap tilemap) {
        //may be replaced by assigning a location to food as well
        Vector3Int mapPos = tilemap.WorldToCell(food.transform.position);

        //if accessible
        if (pop.accessMap[mapPos.x, mapPos.y] == 1) {
            //if edible
            if (pop.foodtypes.Contains(food.getType())) {
                //both accessible and edible so pop consumes food
                return true;
            }
        }
        //pop can't consume the food
        return false;
    }
}
public class Population {
    public Vector3Int location;
    public List<TileBase> accessibleTerrain;
    public int[,] accessMap;
    public List<FoodSource.FoodTypes> foodtypes;
}
public class Position {
    public Vector3 pos;
    public float dist;
}