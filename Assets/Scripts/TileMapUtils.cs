using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class TileMapUtils : MonoBehaviour
{
    private TileMapUtils() { }

    public static Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }
}
