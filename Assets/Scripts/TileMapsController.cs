using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//TODO: 

public enum GameTile { Sand, Grass, Dirt, Stone }

public class TileMapsController : MonoBehaviour
{
    public Tile SelectedTile;

    [SerializeField] private Tilemap _liquid;
    [SerializeField] private Tilemap _terrain;
    [SerializeField] private Tilemap _structures;
    [SerializeField] private Tilemap _tilePlacementPreview;

    private BoundsInt lastBounds = new BoundsInt();
    private Vector3Int lastTile = Vector3Int.zero;

    private Vector3 initialMousePosition = Vector3.zero;

    Dictionary<string, string> TileToTileMap = new Dictionary<string, string>();

    private void Start()
    {
        TextAsset tileNameToMapCSV = Resources.Load<TextAsset>("Text/tileNameToMap");
        string[] tiles = tileNameToMapCSV.text.Split('\n');
        for(int i = 0; i < tiles.Length - 1; i++)
        {
            string[] row = tiles[i].Split(',');
            string terrainName = row[1];
            terrainName = terrainName.Trim(new char[] { ' ' , '\r'}); // Removes \r at the end of the word, maybe see if there is another way to solve this.
            TileToTileMap.Add(row[0], terrainName);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 initialMouseWorldPosition = Camera.main.ScreenToWorldPoint(initialMousePosition);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 bottomLeft = Vector3.Min(initialMouseWorldPosition, mouseWorldPosition);
            Vector3 topRight = Vector3.Max(initialMouseWorldPosition, mouseWorldPosition);

            BoundsInt bounds = new BoundsInt
            {
                min = Vector3Int.FloorToInt(bottomLeft),
                max = Vector3Int.CeilToInt(topRight),
                zMin = 10,
                zMax = 9
            };

            if(bounds != lastBounds)
            {
                _tilePlacementPreview.ClearAllTiles();
            }

            int size = bounds.size.x * bounds.size.y;
            size = size == 0 ? 1 : size;

            TileBase[] tileArray = new TileBase[size];

            for (int i = 0; i < tileArray.Length; i++) tileArray[i] = SelectedTile;
            _tilePlacementPreview.SetTilesBlock(bounds, tileArray);
        }
        else if (mousePositionInTileMap(_tilePlacementPreview) != lastTile)
        {
            // TODO: See if setting tile to null works
            _tilePlacementPreview.SetTile(lastTile, null);
            lastTile = mousePositionInTileMap(_tilePlacementPreview);
            _tilePlacementPreview.SetTile(lastTile, SelectedTile);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _tilePlacementPreview.ClearAllTiles();

            Vector3 initialMouseWorldPosition = Camera.main.ScreenToWorldPoint(initialMousePosition);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 bottomLeft = Vector3.Min(initialMouseWorldPosition, mouseWorldPosition);
            Vector3 topRight = Vector3.Max(initialMouseWorldPosition, mouseWorldPosition);

            BoundsInt bounds = new BoundsInt
            {
                min = Vector3Int.FloorToInt(bottomLeft),
                max = Vector3Int.CeilToInt(topRight),
                zMin = 10,
                zMax = 9
            };

            int size = bounds.size.x * bounds.size.y;
            size = size == 0 ? 1 : size;

            TileBase[] tileArray = new TileBase[size];

            for (int i = 0; i < tileArray.Length; i++) tileArray[i] = SelectedTile;
            PlaceTileBlock(bounds, tileArray);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _tilePlacementPreview.ClearAllTiles();
        }
    }

    public void LeftMousePressed()
    {
        //PlaceTileOnCursor(SelectedTile);
    }

    public void PlaceTile(Vector3Int position, Tile tile)
    {
        string tileMapName = TileToTileMap[tile.name];

        if(tileMapName == "Liquid")
        {
            _liquid.SetTile(position, tile);
        }
        else if(tileMapName == "Terrain")
        {
            _terrain.SetTile(position, tile);
        }
        else if(tileMapName == "Structures")
        {
            _structures.SetTile(position, tile);
        }
        else
        {
            Debug.Log("Tried to place tile on tile map with invalid name: " + tileMapName);
        }
    }

    public void PlaceTileBlock(BoundsInt bounds, TileBase[] tiles)
    {
        string tileMapName = TileToTileMap[tiles[0].name];

        if (tileMapName == "Liquid")
        {
            _liquid.SetTilesBlock(bounds, tiles);
        }
        else if (tileMapName == "Terrain")
        {
            _terrain.SetTilesBlock(bounds, tiles);
        }
        else if (tileMapName == "Structures")
        {
            _structures.SetTilesBlock(bounds, tiles);
        }
        else
        {
            Debug.Log("Tried to place tileblock on tile map with invalid name: " + tileMapName);
        }
    }

    public void PlaceTileOnCursor(Tile tile)
    {
        var worldPosition = TileMapUtils.MouseWorldPosition();
        var position = _terrain.WorldToCell(worldPosition);
        this.PlaceTile(position, tile);
    }

    private Vector3Int mousePositionInTileMap(Tilemap tileMap)
    {
        return tileMap.WorldToCell(TileMapUtils.MouseWorldPosition());
    }
}
