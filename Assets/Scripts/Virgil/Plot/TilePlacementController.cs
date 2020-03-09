using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacementController : MonoBehaviour
{
    // Can be either pen or block mode.
    public bool isBlockMode { get; set; } = false;
    public TerrainTile selectedTile { get; set; } = default;
    [SerializeField] private Camera currentCamera = default;
    private bool isPreviewing { get; set; } = false;
    private Vector3Int dragStartPosition = Vector3Int.zero;
    private Vector3Int lastMouseCellPosition = Vector3Int.zero;
    private Grid grid;
    public List<Tilemap> tilemapList { get { return tilemaps; } }
    [SerializeField] private List<Tilemap> tilemaps = new List<Tilemap>();
    private Dictionary<int, List<Vector3Int>> addedTiles = new Dictionary<int, List<Vector3Int>>();
    private Dictionary<int, Dictionary<Vector3Int, TerrainTile>> removedTiles = new Dictionary<int, Dictionary<Vector3Int, TerrainTile>>();

    private void Awake()
    {
        grid = GetComponent<Grid>();
        foreach (int layer in (int[])Enum.GetValues(typeof(TerrainTile.TileLayer)))
        {
            addedTiles.Add(layer, new List<Vector3Int>());
            removedTiles.Add(layer, new Dictionary <Vector3Int, TerrainTile> ());
        }
    }
    void Update()
    {
        if (isPreviewing)
        {
            Vector3 mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int currentMouseCellPosition = grid.WorldToCell(mouseWorldPosition);
            if (currentMouseCellPosition != lastMouseCellPosition)
            {
                if (isBlockMode)
                {
                    UpdatePreviewBlock();
                }
                else
                {
                    UpdatePreviewPen();
                }
                lastMouseCellPosition = currentMouseCellPosition;
            }
        }
    }
    public void StartPreview(TerrainTile newTile)
    {
        isPreviewing = true;
        selectedTile = newTile;
        Vector3 mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        dragStartPosition = grid.WorldToCell(mouseWorldPosition);
    }
    public void StopPreview()
    {
        isPreviewing = false;
        lastMouseCellPosition = Vector3Int.zero;
        addedTiles.Clear();
        removedTiles.Clear();
        foreach (int layer in (int[])Enum.GetValues(typeof(TerrainTile.TileLayer)))
        {
            addedTiles.Add(layer, new List<Vector3Int>());
            removedTiles.Add(layer, new Dictionary<Vector3Int, TerrainTile>());
        }
    }
    public void RevertChanges()
    {
        foreach (int layer in addedTiles.Keys)
        {
            foreach (Vector3Int cellLocatoion in addedTiles[layer])
            {
                tilemaps[layer].SetTile(cellLocatoion, null);
            }
        }

        foreach (int layer in removedTiles.Keys)
        {
            foreach (KeyValuePair<Vector3Int, TerrainTile> removedTile in removedTiles[layer])
            {
                tilemaps[layer].SetTile(removedTile.Key, removedTile.Value);
            }
        }
        StopPreview();
    }

    private void UpdatePreviewPen()
    {
        Vector3 mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int currentMouseCellPosition = grid.WorldToCell(mouseWorldPosition);
        PlaceTile(currentMouseCellPosition, selectedTile);
    }
    private void UpdatePreviewBlock()
    {
        Vector3 mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseLocalPosition = grid.WorldToLocal(mouseWorldPosition);
        Vector3Int roundedMouseCellPosition = Utils.SignsRoundToIntVector3(mouseLocalPosition, dragStartPosition);
        Vector3Int max = Vector3Int.Max(dragStartPosition + Vector3Int.one, roundedMouseCellPosition);
        Vector3Int min = Vector3Int.Min(dragStartPosition, roundedMouseCellPosition);
        Vector3Int size = max - min;

        size.z = 1;
        BoundsInt bounds = new BoundsInt(min, size);
        DrawBlock(bounds, selectedTile);
    }
    private void DrawBlock(BoundsInt bounds, TerrainTile tile)
    {
        for (var x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (var y = bounds.yMin; y < bounds.yMax; y++)
            {
                PlaceTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
    private void PlaceTile(Vector3Int cellLocation, TerrainTile tile)
    {
        int tileLayer = (int)tile.tileLayer;

        Tilemap targetTilemap = tilemaps[tileLayer];

        foreach (int layer in tile.replacementLayers)
        {
            TerrainTile removedTile = (TerrainTile)tilemaps[layer].GetTile(cellLocation);
            if (!removedTiles[layer].ContainsKey(cellLocation))
            {
                removedTiles[layer].Add(cellLocation, removedTile);
            }
            tilemaps[layer].SetTile(cellLocation, null);
        }

        if (tile != (TerrainTile)targetTilemap.GetTile(cellLocation))
        {
            foreach (int layer in tile.constraintLayers)
            {
                if (tilemaps[layer].HasTile(cellLocation))
                {
                    addedTiles[tileLayer].Add(cellLocation);
                    targetTilemap.SetTile(cellLocation, tile);
                }
            }
            foreach (TerrainTile auxillaryTile in tile.auxillaryTiles)
            {
                addedTiles[(int)auxillaryTile.tileLayer].Add(cellLocation);
                tilemaps[(int)auxillaryTile.tileLayer].SetTile(cellLocation, auxillaryTile);
                addedTiles[(int)auxillaryTile.tileLayer].Add(cellLocation);
            }
            if (tile.constraintLayers.Count == 0)
            {
                TerrainTile removedTile = (TerrainTile)tilemaps[tileLayer].GetTile(cellLocation);
                if (!removedTiles[tileLayer].ContainsKey(cellLocation))
                {
                    removedTiles[tileLayer].Add(cellLocation, removedTile);
                }
                addedTiles[tileLayer].Add(cellLocation);
                targetTilemap.SetTile(cellLocation, tile);
            }
        }
    }
}
