using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//A manager for managing how the reserve is "separated" for each population
public class ReservePartitionManager : MonoBehaviour
{
    List<Population> pops;

    //Check if a population can access toPos
    public static bool CanAccess(Vector3 toPos, Population pop, Tilemap tilemap)
    {
        //convert to map position
        Vector3Int mapPos = tilemap.WorldToCell(toPos);

        //if accessible
        if (pop.GetMap().Contains(mapPos)){
            return true;
        }else{
            return false;
        }
    }

    //Go through pops and return a list of populations that has access to the tile corresponding to toPos
    public List<Population> GetPopulationsWithAccessTo(Vector3 toPos, Tilemap tilemap) {
        List<Population> accessible = new List<Population>();
        foreach (Population pop in pops) {
            //utilize CanAccess()
            if (CanAccess(toPos, pop, tilemap)) {
                accessible.Add(pop);
            }
        }
        return accessible;
    }

    //Populate the access map for a population with breadth first search
    public static List<Vector3Int> GenerateMap(Population pop, Tilemap tilemap) {
        Stack<Vector3Int> queue = new Stack<Vector3Int>();
        List<Vector3Int> accessible = new List<Vector3Int>();
        List<Vector3Int> unaccessible = new List<Vector3Int>();
        Vector3Int cur;

        //starting location
        queue.Push(pop.location);

        //iterate until no tile left in list, ends in iteration 1 if pop.location is not accessible
        while (queue.Count > 0) {
            //next point
            cur = queue.Pop();

            if (accessible.Contains(cur) || unaccessible.Contains(cur)){
                //checked before, move on
                continue;
            }

            //check if tilemap has tile and if pop can access the tile (e.g. some cannot move through water)
            if (tilemap.HasTile(cur) && pop.accessibleTerrain.Contains(tilemap.GetTile(cur)))
            {
                //save the Vector3Int since it is already checked
                accessible.Add(cur);

                //check all 4 tiles around, may be too expensive/awaiting optimization
                queue.Push(cur + Vector3Int.left);
                queue.Push(cur + Vector3Int.up);
                queue.Push(cur + Vector3Int.right);
                queue.Push(cur + Vector3Int.down);
            }
            else {
                //save the Vector3Int since it is already checked
                unaccessible.Add(cur);
            }
        }
        return accessible;

    }

    
    //Check if a population can consume a food
    public static bool Consumes(Population pop, FoodSource food, Tilemap tilemap) {
        //may be replaced by assigning a location to food as well
        Vector3Int mapPos = tilemap.WorldToCell(food.transform.position);

        //if accessible
        if (pop.GetMap().Contains(mapPos)) {
            //if edible
            if (pop.foodtypes.Contains(food.getType())) {
                //both accessible and edible so pop consumes food
                return true;
            }
        }
        //pop can't consume the food
        return false;
    }

    public List<Population> GetConsumers(FoodSource food, Tilemap tilemap) {
        List<Population> consumers = new List<Population>();

        //may be replaced by assigning a location to food as well
        Vector3Int mapPos = tilemap.WorldToCell(food.transform.position);

        foreach (Population pop in pops) {
            if (Consumes(pop, food, tilemap)) {
                consumers.Add(pop);
            }
        }

        return consumers;
    }
}