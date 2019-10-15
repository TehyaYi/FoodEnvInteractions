using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GameTile { Sand, Grass, Dirt, Stone }

public class TileMapsController : MonoBehaviour
{
    public Tile SelectedTile;

    [SerializeField]
    private Grid _tileMapsGrid;
    private Tilemap _terrain;

    void Start()
    { 
        _terrain = _tileMapsGrid.transform.GetChild(1).GetComponent<Tilemap>();
    }

    void Update()
    {
        
    }

    public void LeftMousePressed()
    {
        PlaceSelectedTileOnCursor();
    }

    public void PlaceTile(Vector3Int position, Tile tile)
    {
        _terrain.SetTile(position, tile);
    }

    public void PlaceTileOnCursor(Tile tile)
    {
        Vector3Int position = new Vector3Int();
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10));
        position = _terrain.WorldToCell(worldPosition);
        this.PlaceTile(position, tile);
    }

    public void PlaceSelectedTileOnCursor()
    {
        PlaceTileOnCursor(SelectedTile);
    }
}
