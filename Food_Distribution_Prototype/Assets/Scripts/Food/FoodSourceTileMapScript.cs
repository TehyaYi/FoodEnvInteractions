using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoodSourceTileMapScript : MonoBehaviour
{
    private Tilemap tileMap;
    private TileBase[] tiles;

    // Start is called before the first frame update
    void Awake()
    {
        tileMap = this.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<FoodSource> getFoodSources()
    {
        BoundsInt bounds = tileMap.cellBounds;
        tiles = tileMap.GetTilesBlock(bounds);
        List<FoodSource> foodSources = new List<FoodSource>();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (tile.name == FoodSources.SPACE_MAPLE_TILE_NAME)
                    {
                        foodSources.Add(new SpaceMaple());
                    }
                    else if(tile.name == FoodSources.FRUIT_TREE_TILE_NAME)
                    {
                        foodSources.Add(new FruitTree());
                    }
                    else if(tile.name == FoodSources.ARID_BUSH_TILE_NAME)
                    {
                        foodSources.Add(new AridBush());
                    }
                }
            }
        }

        return foodSources;
    }
}
