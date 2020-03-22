using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRetriever : MonoBehaviour
{
    /// <summary>
    /// Get TerrainTiles at world_pos with a radius of radius.
    /// </summary>
    public static List<TerrainTile> GetTiles(Vector3 world_pos, int radius) {
        GetTerrainTile api = FindObjectOfType<GetTerrainTile>();

        //list of tiles to return
        List<TerrainTile> tiles = new List<TerrainTile>();

        //position of object in terms of tilemap
        Vector3Int cell_pos = ReservePartitionManager.ins.WorldToCell(world_pos);

        //prototype nested loop -- could be a little more efficient
        for (int r = cell_pos.y - radius; r <= cell_pos.y + radius; r++) {
            for (int c = cell_pos.x - radius; c <= cell_pos.x + radius; c++) {
                //if in terms of abs distance
                //float dist = Mathf.Sqrt(Mathf.Pow(r-cell_pos.y,2) + Mathf.Pow(c-cell_pos.x,2));

                //tile-based distance
                int dist = Mathf.Abs(r - cell_pos.y) + Mathf.Abs( c - cell_pos.x );

                //tile is within range: get it
                if (dist <= radius)
                {
                    Vector3Int pos = new Vector3Int(c, r, 0);
                    //if there's a tile -- condition may be changed later, such as if the tile is a certain type
                    TerrainTile tile = api.GetTerrainTileAtLocation(pos);
                    if (tile != null) 
                    {
                        tiles.Add(tile);//Get the tile
                    }
                }
            }

        }
        return tiles;
    }
}