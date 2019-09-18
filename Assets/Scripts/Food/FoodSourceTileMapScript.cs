using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class FoodSourceTileMapScript : MonoBehaviour
{
    private Tilemap tileMap;
    private Tilemap terrainMap;
    private TileBase[] tiles;
    private TileBase[] terrainTiles;
    SpaceMapleCalc SpaceMapleCalc;
    FruitTreeCalc FruitTreeCalc;
    LeafTreeCalc LeafTreeCalc;
    BerryTreeCalc BerryTreeCalc;
    TallgrassCalc TallgrassCalc;
    BerryBushCalc BerryBushCalc;
    AridBushCalc AridBushCalc;
    LeafyBushCalc LeafyBushCalc;
    [SerializeField] Canvas Canvas;
    [SerializeField] GameObject FoodSourceOutput;
    [SerializeField] int xVal;
    [SerializeField] int yVal;
    [SerializeField] int zVal;
    [SerializeField] int tempVal;
    [SerializeField] int lightVal;

    void Awake()
    {
        tileMap = this.GetComponent<Tilemap>();
        terrainMap = GameObject.Find("Terrain").GetComponent<Tilemap>();
        SpaceMapleCalc = new SpaceMapleCalc();
        FruitTreeCalc = new FruitTreeCalc();
        LeafTreeCalc = new LeafTreeCalc();
        BerryTreeCalc = new BerryTreeCalc();
        TallgrassCalc = new TallgrassCalc();
        BerryBushCalc = new BerryBushCalc();
        AridBushCalc = new AridBushCalc();
        LeafyBushCalc = new LeafyBushCalc();
    }

    // Detects all foodsource tiles and place
    public List<FoodSource> getFoodSources()
    {
        BoundsInt bounds = terrainMap.cellBounds;
        tiles = tileMap.GetTilesBlock(bounds);
        terrainTiles = terrainMap.GetTilesBlock(bounds);
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
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x,y), bounds, 4);
                        int modifiedOutput = SpaceMapleCalc.modifyOutput(xVal, yVal, zVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new SpaceMaple(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53, y - 37));
                    }
                    else if(tile.name == FoodSourceTileNames.FRUIT_TREE_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 4);
                        int modifiedOutput = FruitTreeCalc.modifyOutput(tempVal, lightVal, terrainTiles);
                        foodSources.Add(new FruitTree(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53, y - 37));
                    }
                    else if(tile.name == FoodSourceTileNames.LEAF_TREE_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 4);
                        int modifiedOutput = LeafTreeCalc.modifyOutput(yVal, zVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new LeafTree(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53, y - 37));
                    }
                    else if (tile.name == FoodSourceTileNames.BERRY_TREE_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 4);
                        int modifiedOutput = BerryTreeCalc.modifyOutput(yVal, zVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new BerryTree(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53, y - 37));
                    }
                    else if (tile.name == FoodSourceTileNames.TALL_GRASS_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 1);
                        int modifiedOutput = TallgrassCalc.modifyOutput(yVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new Tallgrass(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53.5f, y - 36.8f));
                    }
                    else if (tile.name == FoodSourceTileNames.BERRY_BUSH_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 1);
                        int modifiedOutput = BerryBushCalc.modifyOutput(yVal, zVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new BerryBush(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53.5f, y - 36.8f));
                    }
                    else if (tile.name == FoodSourceTileNames.ARID_BUSH_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 1);
                        int modifiedOutput = AridBushCalc.modifyOutput(tempVal, lightVal, terrainTiles);
                        foodSources.Add(new AridBush(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53.5f, y - 36.8f));
                    }
                    else if (tile.name == FoodSourceTileNames.LEAFY_BUSH_TILE_NAME)
                    {
                        Dictionary<string, int> terrainTiles = getTerrainTiles(new Vector2Int(x, y), bounds, 2);
                        int modifiedOutput = LeafyBushCalc.modifyOutput(yVal, zVal, tempVal, lightVal, terrainTiles);
                        foodSources.Add(new LeafyBush(tilePosition, modifiedOutput));
                        displayResult(modifiedOutput, new Vector2(x - 53.0f, y - 36.8f));
                    }
                }
            }
        }

        return foodSources;
    }

    private Dictionary<string, int> getTerrainTiles(Vector2Int pos, BoundsInt bounds, int foodSize)
    {
        int sand = 0;
        int grass = 0;
        int dirt = 0;
        int rock = 0;

        int n = 0;
        int m = 0;

        switch(foodSize)
        {
            case 1:
                n = 1;
                m = 1;
                break;
            case 2:
                n = 2;
                m = 1;
                break;
            case 4:
                n = 2;
                m = 2;
                break;
        }
        for (int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
            {
                TileBase tile = terrainTiles[(pos.x + i) + (pos.y + j) * bounds.size.x];
                if (tile != null)
                {
                    switch (tile.name)
                    {
                        case "Sand":
                            sand++;
                            break;
                        case "Grass":
                            grass++;
                            break;
                        case "Dirt":
                            dirt++;
                            break;
                        case "Rock":
                            rock++;
                            break;
                    }
                }
            }
        }

        Dictionary<string, int> tiles = new Dictionary<string, int>();
        tiles.Add("sand", sand);
        tiles.Add("grass", grass);
        tiles.Add("dirt", dirt);
        tiles.Add("rock", rock);

        return (tiles);
    }

    private void displayResult(int productionOutput, Vector2 position)
    {
        // Create the text prefab and set the text string to the production value
        Text productionText = Instantiate(FoodSourceOutput).GetComponent<Text>();
        productionText.text = productionOutput.ToString();

        // Place the text at the tile's position (make the parent the Canvas so it shows up in the game)
        productionText.transform.SetParent(Canvas.transform, false);
        productionText.transform.position = position;
    }
}
