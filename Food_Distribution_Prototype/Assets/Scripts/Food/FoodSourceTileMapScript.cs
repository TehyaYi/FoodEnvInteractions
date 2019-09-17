using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoodSourceTileMapScript : MonoBehaviour
{
    private Tilemap tileMap;
    private TileBase[] tiles;

    void Awake()
    {
        tileMap = this.GetComponent<Tilemap>();
    }

    // Detects all foodsource tiles and place
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
                Vector2 tilePosition = new Vector2(x, y);
                if (tile != null)
                {
                    if (tile.name == FoodSourceTileNames.SPACE_MAPLE_TILE_NAME)
                    {
                        foodSources.Add(new SpaceMaple(tilePosition, SpaceMaple.baseOutput)); // 1 TODO: second argument of constructors is where their modified outputs should go.
                    }
                    else if(tile.name == FoodSourceTileNames.FRUIT_TREE_TILE_NAME)
                    {
                        foodSources.Add(new FruitTree(tilePosition, FruitTree.baseOutput)); // 2
                    }
                    else if(tile.name == FoodSourceTileNames.ARID_BUSH_TILE_NAME)
                    {
                        foodSources.Add(new AridBush(tilePosition, AridBush.baseOutput)); // 3
                    }
                }
            }
        }

        return foodSources;
    }
}
