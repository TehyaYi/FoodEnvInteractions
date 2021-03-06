﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRetriever : MonoBehaviour
{
    public Tilemap terrain;
    public Tilemap liquid;

    public Tilemap GetTerrain() { return terrain; }
    public Tilemap GetLiquid() { return liquid; }

    //Get tiles at world_pos with a radius of radius
    public List<TileBase> GetTiles(Vector3 world_pos, int radius, Tilemap from) {
        //list of tiles to return
        List<TileBase> tiles = new List<TileBase>();

        //position of object in terms of tilemap
        Vector3Int cell_pos = from.WorldToCell(world_pos);

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
                    if (from.HasTile(pos)) 
                    {
                        tiles.Add(from.GetTile(pos));//Get the tile
                    }
                }
            }

        }
        return tiles;
    }
    public List<TileBase> GetTerrainTiles(Vector3 world_pos, int radius) { return GetTiles(world_pos, radius, terrain); }
    public List<TileBase> GetLiquidTiles(Vector3 world_pos, int radius) { return GetTiles(world_pos, radius, liquid); }

}