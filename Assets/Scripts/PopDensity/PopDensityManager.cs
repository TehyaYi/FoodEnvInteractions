using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A manager for calculating the population density of each population.
/// </summary>
public class PopDensityManager : MonoBehaviour
{
    //singleton
    public static PopDensityManager ins;


    //for easy access, equivalent to ReservePartitionManager.ins
    ReservePartitionManager rpm;

    //Dictionary<ID, population> initialized from rpm
    Dictionary<int, Population> popsByID;

    //Density map based on what species spreads throughout the area, similar to AccessMap in rpm
    Dictionary<Vector3Int, long> popDensityMap;

    //if in demo
    public bool PDMDemo; 

    public void Awake()
    {
        //singleton
        if (ins != null && this != ins)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    public void Start()
    {
        rpm = ReservePartitionManager.ins;
        Invoke("Init", 0.1f);
    }
    /// <summary>
    /// Initialize variables from rpm and generate new density map
    /// Has to be separate from start to allow populations to be added to the rpm
    /// </summary>
    public void Init() {
        popsByID = new Dictionary<int, Population>();
        List<Population> pops = rpm.getPops();
        foreach (Population pop in pops)
        {
            popsByID.Add(pop.GetID(), pop);
        }
        GenerateDensityMap();

        //graph the density map if in demo
        if(PDMDemo)
            Graph();
    }

    /// <summary>
    /// Determine the population density at a certain cell position.
    /// </summary>
    /// <param name="pos"> Cell Position </param>
    // O(n) algorithm
    public float GetPopDensityAt(Vector3Int pos) {
        //if not a key, no population lives there and therefore density is 0
        if (popDensityMap.ContainsKey(pos))
        {
            float density = 0;
            //accumulate the weight/tile (≈ density) of populations there
            for (int i = 0; i < 64; i++)
            {
                //the pop lives there, add its weight/tile to density
                if (((popDensityMap[pos] >> i) & 1L) == 1L)
                {
                    Population cur = popsByID[i];
                    density += cur.GetWeight() / cur.GetSpace();
                }
            }
            return density;
        }
        else {
            return 0f;
        }
    }

    /// <summary>
    /// Generate the Density Map, only called by running Init()
    /// </summary>
    //(2r^2 + 2r + 1) * O(n) algorithm, significantly more expensive if radius is big
    private void GenerateDensityMap()
    {
        popDensityMap = new Dictionary<Vector3Int, long>();

        List<Population> pops = rpm.getPops();

        foreach (Population pop in pops)
        {
            GenerateDensityMap(pop);
        }
    }

    private void GenerateDensityMap(Population pop) {
        Vector3Int cellPos = rpm.WorldToCell(pop.transform.position);

        //find the number of accessible tiles within radius (≈ living space) and populate Density Map
        int radius = pop.GetRadius();
        int space = 0;
        //loop through each tile within radius
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius + Mathf.Abs(x); y <= radius - Mathf.Abs(x); y++)
            {
                Vector3Int pos = new Vector3Int(cellPos.x + x, cellPos.y + y, 0);
                //if accessible, add space and add to density map
                if (rpm.CanAccess(pop, pos))
                {
                    space++;
                    if (popDensityMap.ContainsKey(pos))
                    {
                        popDensityMap[pos] |= 1L << pop.GetID();
                    }
                    else
                    {
                        popDensityMap.Add(pos, 1L << pop.GetID());
                    }
                }
            }
        }
        //save the amount of space the pop has
        pop.SetSpace(space);
    }

    //(2r^2 + 2r + 1) * O(n) algorithm, significantly more expensive if radius is big
    public float GetDensityScore(Population pop) {
        //not initialized
        if (!rpm.getPops().Contains(pop))
            return -1;

        //calculate the number of accessible tiles within radius (≈ living space)
        int radius = pop.GetRadius();
        float density = 0;
        //loop through each tile within radius, sum up weight from all of them
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius + Mathf.Abs(x); y <= radius - Mathf.Abs(x); y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (rpm.CanAccess(pop, pos))
                {
                    //add population density at the tile to density, note that density/tile * 1 tile = weight
                    //so this is summing the weight at this point
                    density += GetPopDensityAt(pos);
                }
            }
        }

        //total weight / tiles = density
        density /= pop.GetSpace();
        return density;
    }

    /// <summary>
    /// Mask for showing the demo.
    /// </summary>
    public Tilemap mask;

    /// <summary>
    /// Graphing for demo purposes, may be worked into the game as a sort of inspection mode?
    /// </summary>
    public void Graph() {
        //colors
        Dictionary<Vector3Int, float> col = new Dictionary<Vector3Int, float>();

        //max density for color comparison
        float maxDensity = -1;

        //find max density and calculate density for each tile
        foreach (KeyValuePair<Vector3Int, long> pair in popDensityMap)
        {
            //calculate density
            float density = GetPopDensityAt(pair.Key);

            col.Add(pair.Key, density);

            if (density > maxDensity) {
                maxDensity = density;
            }
        }

        //set color based on the fraction density/maxdensity
        foreach (KeyValuePair<Vector3Int, float> pair in col)
        {
            //By default the flag is TileFlags.LockColor
            mask.SetTileFlags(pair.Key, TileFlags.None);

            //set color of tile, close to maxDensity = red, close to 0 = green, in the middle = orange
            mask.SetColor(pair.Key, new Color(pair.Value/maxDensity, 1-pair.Value/maxDensity, 0, 255.0f/255));
        }

        //debug
        print(maxDensity);
    }
}
