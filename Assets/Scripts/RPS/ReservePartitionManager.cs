using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A manager for managing how the reserve is "separated" for each population.
/// </summary>
public class ReservePartitionManager : MonoBehaviour
{
    //singleton
    public static ReservePartitionManager ins;

    List<Population> pops;
    Stack<int> openID;
    Dictionary <Vector3Int, long> accessMap;
    public Tilemap terrain;
    public Tilemap liquid; //currently not used

    public void Awake() {
        if (ins != null && this != ins)
        {
            Destroy(this);
        }
        else {
            ins = this;
        }

        //long mask is limited to 64 bits
        openID = new Stack<int>();
        for (int i = 63; i >= 0 ; i--) {
            openID.Push(i);
        }
        pops = new List<Population>();
        accessMap = new Dictionary<Vector3Int, long>();
    }

    ///<summary>
    ///Add a population to the RPM.
    ///</summary>
    public void AddPopulation(Population pop) {
        if (!pops.Contains(pop)){
            //ignore their old id and assign it a new one
            AssignID(pop);

            //generate the map with the new id  
            GenerateMap(pop);
        }
    }

    private void AssignID(Population pop) {
        pop.setID(openID.Pop());
        pops.Add(pop);
    }

    ///<summary>
    ///Remove a population from the RPM
    ///</summary>
    public void RemovePopulation(Population pop) {
        pops.Remove(pop);
        openID.Push(pop.getID()); //free ID
    }

    ///<summary>
    ///Populate the access map for a population with depth first search.
    ///</summary>
    private void GenerateMap(Population pop) {

        if (!pops.Contains(pop)) {
            AssignID(pop);
        }
        Stack<Vector3Int> stack = new Stack<Vector3Int>();
        List<Vector3Int> accessible = new List<Vector3Int>();
        List<Vector3Int> unaccessible = new List<Vector3Int>();
        Vector3Int cur;

        //starting location
        Vector3Int location = terrain.WorldToCell(pop.transform.position);
        stack.Push(location);

        //iterate until no tile left in list, ends in iteration 1 if pop.location is not accessible
        while (stack.Count > 0) {
            //next point
            cur = stack.Pop();

            if (accessible.Contains(cur) || unaccessible.Contains(cur)){
                //checked before, move on
                continue;
            }

            //check if tilemap has tile and if pop can access the tile (e.g. some cannot move through water)
            //implementation may change when liquid gets added
            if (terrain.HasTile(cur) && pop.accessibleTerrain.Contains(terrain.GetTile(cur)))
            {
                //save the Vector3Int since it is already checked
                accessible.Add(cur);

                //check all 4 tiles around, may be too expensive/awaiting optimization
                stack.Push(cur + Vector3Int.left);
                stack.Push(cur + Vector3Int.up);
                stack.Push(cur + Vector3Int.right);
                stack.Push(cur + Vector3Int.down);
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

    }

    ///<summary>
    ///Update the access map for every population in pops
    ///</summary>
    public void UpdateAccessMap()
    {
        foreach (Population pop in pops)
        {
            GenerateMap(pop);
        }
    }

    ///<summary>
    ///Check if a population can access toWorldPos.
    ///</summary>
    public bool CanAccess(Population pop, Vector3 toWorldPos)
    {
        //convert to map position
        Vector3Int mapPos = terrain.WorldToCell(toWorldPos);

        //if accessible
        //check if the nth bit is set (i.e. accessible for the pop)
        if (accessMap.ContainsKey(mapPos))
        {
            if (((accessMap[mapPos] >> pop.getID()) & 1L) == 1L)
            {
                return true;
            }
        }

        //pop can't access the position
        return false;
    }

    ///<summary>
    ///Go through pops and return a list of populations that has access to the tile corresponding to toWorldPos.
    ///</summary>
    public List<Population> GetPopulationsWithAccessTo(Vector3 toWorldPos)
    {
        List<Population> accessible = new List<Population>();
        foreach (Population pop in pops)
        {
            //utilize CanAccess()
            if (CanAccess(pop, toWorldPos))
            {
                accessible.Add(pop);
            }
        }
        return accessible;
    }

    ///<summary>
    ///[Deprecated] Check if a population can consume a food.
    ///</summary>
    public bool Consumes(Population pop, FoodSource food) {
        //if accessible
        //check if the nth bit is set (i.e. accessible for the pop)
        if (CanAccess(pop, food.transform.position)) {
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
    ///[Deprecated] Go through pops and return a list of populations that can consume a food source.
    ///</summary>
    public List<Population> GetConsumers(FoodSource food) {
        List<Population> consumers = new List<Population>();

        foreach (Population pop in pops) {
            //utilize Consumes()
            if (Consumes(pop, food)) {
                consumers.Add(pop);
            }
        }

        return consumers;
    }
}