using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APITester : MonoBehaviour
{
    // Start is called before the first frame update
    private GetTerrainTile getTerrainTile;
    void Start()
    {
        getTerrainTile = GetComponent<GetTerrainTile>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(getTerrainTile.GetTerrainTileAtLocation(new Vector3Int(2, 2, 0)));
    }
}
