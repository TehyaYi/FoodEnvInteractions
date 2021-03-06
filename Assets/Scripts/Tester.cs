﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public FoodSource info;
    public TileRetriever toTest;
    public Dictionary<string, int> counts;
    public Tilemap from;
    public Text text;
    public Dictionary<string, string> nameSwap;
    

    // Start is called before the first frame update
    void Start()
    {
        // detects the environment every second
        nameSwap = new Dictionary<string, string>();
        nameSwap.Add("TileMap_1", "Grass");
        nameSwap.Add("TileMap_4","Rock");
        nameSwap.Add("TileMap_0", "Dirt");

        from = toTest.GetTerrain();
        InvokeRepeating("UpdatePosition", 0, 0.2f);
    }

    // prints the environment on screen 
    public void UpdatePosition()
    {
        info.DetectEnvironment();
        List<TileBase> ts = toTest.GetTerrainTiles(transform.position, info.foodValues.getRadius());
        counts = new Dictionary<string, int>();
        for (int i = 0; i < ts.Count; i++)
        {
            if (counts.ContainsKey(ts[i].name)) {
                counts[ts[i].name] += 1;
            }
            else
            {
                counts.Add(ts[i].name, 1);
            }
        }
        string end = "";
        end += "Total Output: " + info.getOutput() + "\n";
        end += "Position on Tilemap: "+from.WorldToCell(transform.position) + "\n";
        end += "Terrain raw value: " + info.getRawValues()[0] + "\n";
        foreach (KeyValuePair<string,int> kp in counts){
            string name = nameSwap.ContainsKey(kp.Key) ? nameSwap[kp.Key] : kp.Key;
            end += name + ": " + kp.Value + "\n";
        }
        text.text = end;
    }
}
