using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacementPreview : MonoBehaviour
{

    [SerializeField]
    private Tilemap _tileMap;

    void Start()
    {

    }

    void Update()
    {

    }

    public void DrawTile(Vector2 worldCoordinates, Tile tile)
    {
        Vector3Int cellPosition = new Vector3Int((int) worldCoordinates.x, (int) worldCoordinates.y, 0);
        _tileMap.SetTile(cellPosition, tile);
    }

    public void DrawSquare(Bounds worldCoordinatesBound, Tile tile)
    {
        BoundsInt cellBounds = new BoundsInt
        {
            min = Vector3Int.RoundToInt(worldCoordinatesBound.min),
            max = Vector3Int.RoundToInt(worldCoordinatesBound.max)
        };
        TileBase[] tileArray = { tile };
        _tileMap.SetTilesBlock(cellBounds, tileArray);
    }
}
