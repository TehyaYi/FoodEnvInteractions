using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//A manager for managing how the reserve is "separated" for each population
public class ReservePartitionManager : MonoBehaviour
{
    //singleton
    public static ReservePartitionManager ins;

    List<Population> pops;
    Stack<int> openID;
    Dictionary <Vector3Int, long> accessMap;

    public void Awake() {
        if (ins != null && this != ins)
        {
            Destroy(this);
        }
        else {
            ins = this;
        }
        
        openID = new Stack<int>();
        for (int i = 63; i >= 0 ; i--) {
            openID.Push(i);
        }
        pops = new List<Population>();
        accessMap = new Dictionary<Vector3Int, long>();
    }

    public void AssignID(List<Population> existingPops)
    {
        foreach (Population pop in existingPops) {
            if (!pops.Contains(pop)) {
                //ignore their old id and assign them new ones
                AssignID(pop);
            }
        }
    }

    public void AssignID(Population pop) {
        pop.setID(openID.Pop());
        pops.Add(pop);
    }

    public void FreeID(Population pop) {
        pops.Remove(pop);
        openID.Push(pop.getID());
    }


    ///<summary>Populate the access map for a population with depth first search.</summary>
    public List<Vector3Int> GenerateMap(Population pop, Tilemap tilemap) {

        if (!pops.Contains(pop)) {
            AssignID(pop);
        }
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

        foreach (Vector3Int pos in accessible) {
            if (!accessMap.ContainsKey(pos)) {
                accessMap.Add(pos, 0L);
            }
            //set the pop.getID()th bit in accessMap[pos] to 1
            accessMap[pos] |= 1L << pop.getID();
        }
        return accessible;

    }

    ///<summary>
    ///Check if a population can access toPos.
    ///</summary>
    public bool CanAccess(Population pop, Vector3 toWorldPos, Tilemap tilemap)
    {
        //convert to map position
        Vector3Int mapPos = tilemap.WorldToCell(toWorldPos);

        //if accessible
        //check if the nth bit is set (i.e. accessible for the pop)
        if (accessMap.ContainsKey(mapPos))
        {
            if (((accessMap[mapPos] >> pop.getID()) & 1L) == 1L)
            {
                return true;
            }
        }

        return false;
    }

    ///<summary>
    ///Go through pops and return a list of populations that has access to the tile corresponding to toPos.
    ///</summary>
    public List<Population> GetPopulationsWithAccessTo(Vector3 toPos, Tilemap tilemap)
    {
        List<Population> accessible = new List<Population>();
        foreach (Population pop in pops)
        {
            //utilize CanAccess()
            if (CanAccess(pop, toPos, tilemap))
            {
                accessible.Add(pop);
            }
        }
        return accessible;
    }


    ///<summary>
    ///Check if a population can consume a food.
    ///</summary>
    public bool Consumes(Population pop, FoodSource food, Tilemap tilemap) {
        //if accessible
        //check if the nth bit is set (i.e. accessible for the pop)
        if (CanAccess(pop, food.transform.position, tilemap)) {
            //if edible
            if (pop.foodtypes.Contains(food.getType())) {
                //both accessible and edible so pop consumes food
                return true;
            }
        }
        //pop can't consume the food

        return false;
    }

    ///<summary>
    ///Go through pops and return a list of populations that can consume a food source.
    ///</summary>
    public List<Population> GetConsumers(FoodSource food, Tilemap tilemap) {
        List<Population> consumers = new List<Population>();

        foreach (Population pop in pops) {
            //utilize Consumes()
            if (Consumes(pop, food, tilemap)) {
                consumers.Add(pop);
            }
        }

        return consumers;
    }
}