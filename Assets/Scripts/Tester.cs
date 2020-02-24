using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public FoodSource info;
    public TileRetriever toTest;
    public int radius;
    public Dictionary<string, int> counts;
    public Tilemap from;
    public Text text;
    public Dictionary<string, string> nameSwap;

    // Start is called before the first frame update
    void Start()
    {
        // detects the environment every second
        nameSwap = new Dictionary<string, string>();
        nameSwap.Add("TileMap_4","White");
        nameSwap.Add("TileMap_5", "Red");
        nameSwap.Add("TileMap_0", "Orange");
        nameSwap.Add("TileMap_12", "Gray");
        nameSwap.Add("TileMap_11", "Black");

        from = toTest.GetTilemap();
        InvokeRepeating("UpdatePosition", 0, 0.2f);
    }

    // detects the environment every second
    public void UpdatePosition()
    {
        List<TileBase> ts = toTest.GetTiles(transform.position, radius);
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
        foreach (KeyValuePair<string,int> kp in counts){
            string name = nameSwap.ContainsKey(kp.Key) ? nameSwap[kp.Key] : kp.Key;
            end += name + ": " + kp.Value + "\n";
        }
        text.text = end;
    }
}
