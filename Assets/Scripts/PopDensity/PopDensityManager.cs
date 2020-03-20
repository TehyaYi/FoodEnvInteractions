using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopDensityManager : MonoBehaviour
{
    //singleton
    public static PopDensityManager ins;

    Dictionary<int, Population> popsByID;
    Dictionary<Vector3Int, long> popDensityMap;
    ReservePartitionManager rpm;

    public bool PDMDemo; //in demo
    public void Awake()
    {
        if (ins != null && this != ins)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
        popsByID = new Dictionary<int, Population>();
    }

    public void Start()
    {
        rpm = ReservePartitionManager.ins;
        Invoke("LateStart", 0.1f);
    }

    public void LateStart() {

        List<Population> pops = rpm.getPops();
        foreach (Population pop in pops)
        {
            popsByID.Add(pop.getID(), pop);
        }
        GenerateDensityMap();

        //graph the density map if in demo
        if(PDMDemo)
            Graph();
    }

    //O(n) algorithm
    public float GetPopDensityAt(Vector3Int pos) {
        if (popDensityMap.ContainsKey(pos))
        {
            float density = 0;
            for (int i = 0; i < 64; i++)
            {
                if (((popDensityMap[pos] >> i) & 1L) == 1L)
                {
                    Population cur = popsByID[i];
                    density += cur.getPopSize() / cur.getSpace();
                }
            }
            return density;
        }
        else {
            return 0f;
        }
    }

    //(2r^2 + 2r + 1) * O(n) algorithm, significantly more expensive if radius is big
    public void GenerateDensityMap()
    {
        popDensityMap = new Dictionary<Vector3Int, long>();

        List<Population> pops = ReservePartitionManager.ins.getPops();

        foreach (Population pop in pops)
        {
            Vector3Int cellPos = rpm.GetReferenceCellPos(pop.transform.position);

            //calculate the number of accessible tiles within radius (= living space)
            int radius = pop.getRadius();
            int space = 0;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius + Mathf.Abs(x); y <= radius - Mathf.Abs(x); y++)
                {
                    Vector3Int pos = new Vector3Int(cellPos.x + x, cellPos.y + y, 0);
                    if (rpm.CanAccess(pop, pos))
                    {
                        space++;
                        if (popDensityMap.ContainsKey(pos))
                        {
                            popDensityMap[pos] |= 1L << pop.getID();
                        }
                        else {
                            popDensityMap.Add(pos, 1L << pop.getID());
                        }
                    }
                }
            }
            pop.setSpace(space);
        }

        print("popDensityMap.count = " + popDensityMap.Count);
    }

    //(2r^2 + 2r + 1) * O(n) algorithm, significantly more expensive if radius is big
    public float GetDensityScore(Population pop) {
        //not initialized
        if (!rpm.getPops().Contains(pop))
            return -1;

        //calculate the number of accessible tiles within radius (= living space)
        int radius = pop.getRadius();
        float density = 0;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius + Mathf.Abs(x); y <= radius - Mathf.Abs(x); y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (rpm.CanAccess(pop, pos))
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (((popDensityMap[pos] >> i) & 1L) == 1L) {
                            //potentially deleted population
                            if (popsByID.ContainsKey(i))
                            {
                                Population cur = popsByID[i];
                                density += cur.getPopSize() / cur.getSpace();
                            }
                        }
                    }
                }
            }
        }
        density /= pop.getSpace();
        return density;
    }

    public Tilemap mask;
    public void Graph() {
        Dictionary<Vector3Int, float> col = new Dictionary<Vector3Int, float>();
        float maxDensity = -1;
        foreach (KeyValuePair<Vector3Int, long> pair in popDensityMap)
        {
            float density = GetPopDensityAt(pair.Key);
            if (density > maxDensity) {
                maxDensity = density;
            }
            col.Add(pair.Key, density);
        }
        foreach (KeyValuePair<Vector3Int, float> pair in col)
        {
            mask.SetTileFlags(pair.Key, TileFlags.None);
            mask.SetColor(pair.Key, new Color(pair.Value/maxDensity, 1-pair.Value/maxDensity, 0, 255.0f/255));
        }
        print(maxDensity);
    }
}
