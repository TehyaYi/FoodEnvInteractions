using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tester : MonoBehaviour
{
    public TileRetriever toTest;
    public int rad;
    int ct = 0;
    // Start is called before the first frame update
    void Start()
    {
        // detects the environment every second
        InvokeRepeating("UpdatePosition", 0, 1);
    }

    // detects the environment every second
    void UpdatePosition()
    {
        ct++;
        List<TileBase> ts = toTest.GetTiles(transform.position, rad);
        for (int i = 0; i < ts.Count; i++)
        {
            print("iter " + ct + ": " + ts[i].name);
        }
    }
}
