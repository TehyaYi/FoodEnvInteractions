using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GameTile { Sand, Grass, Dirt, Stone }

public class TileMapsController : MonoBehaviour
{
    public Tile SelectedTile;

    [SerializeField] private Tilemap _liquid;
    [SerializeField] private Tilemap _terrain;
    [SerializeField] private Tilemap _structures;
    [SerializeField] private Tilemap _tilePlacementPreview;

    private BoundsInt lastBounds = new BoundsInt();

    private Vector3 initialMousePosition = Vector3.zero;

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
    }

    public void LeftMousePressed()
    {
        //PlaceTileOnCursor(SelectedTile);
    }

    public void PlaceTile(Vector3Int position, Tile tile)
    {
        _terrain.SetTile(position, tile);
    }

    public void PlaceTileOnCursor(Tile tile)
    {
        var worldPosition = TileMapUtils.MouseWorldPosition();
        var position = _terrain.WorldToCell(worldPosition);
        this.PlaceTile(position, tile);
    }
}
