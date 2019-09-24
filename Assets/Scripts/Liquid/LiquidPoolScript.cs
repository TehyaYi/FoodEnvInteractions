using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LiquidPoolScript : MonoBehaviour
{
    private Tilemap liquidTileMap;
    private BoundsInt bounds;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);    
    }

    // Start is called before the first frame update
    void Start()
    {
        liquidTileMap = GetComponent<Tilemap>();
        bounds = liquidTileMap.cellBounds;
        TileBase[] allTiles = liquidTileMap.GetTilesBlock(bounds);
        Dictionary<TileBase, bool> liquidTiles = new Dictionary<TileBase, bool>();
        Debug.Log("Num Tiles: " + allTiles.Length);
        for(int x = 0; x < bounds.size.x; x++)
        {
            for(int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + (y * bounds.size.x)];
                if(tile != null)
                {
                    Debug.Log(tile.name);
                    GridLayout gridLayout = liquidTileMap.layoutGrid;
                    GameObject thign = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    thign.transform.position = new Vector3(x + bounds.position.x, y + bounds.position.y, -3);
                    //liquidTiles.Add(tile, false);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GroupLiquidTiles(TileBase[] allTiles, Dictionary<TileBase, bool> liquidTiles)
    {
        //GameObject newPool = new GameObject();
        //void dfs(KeyValuePair<TileBase, bool> liquidTile)
        //{
        //    if(liquidTile.Value == true)
        //    {
        //        return;
        //    }
        //    else
        //    {

        //    }
        //}
    }
}

public class LiquidTile : TileBase{
    public bool InPool = false;
}
