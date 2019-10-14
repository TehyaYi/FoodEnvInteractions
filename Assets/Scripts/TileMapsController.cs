using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GameTile { Sand, Grass, Dirt, Stone }

public class TileMapsController : MonoBehaviour
{
    [SerializeField]
    private Grid _tileMapsGrid;
    public Tile tile;

    private Tilemap _terrain;

    // Start is called before the first frame update
    void Start()
    {
        _terrain = _tileMapsGrid.transform.GetChild(1).GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTile()
    {
        Vector3Int position = new Vector3Int();
        Vector3 screenPosition = Input.mousePosition;
        Debug.Log("Screen Position: " + screenPosition);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10));
        Debug.Log("World Position: " + worldPosition);
        position = _terrain.WorldToCell(worldPosition);
        Debug.Log("Cell Position: " + position);
        Debug.Log(position);
        _terrain.SetTile(position, tile);
    }
}
