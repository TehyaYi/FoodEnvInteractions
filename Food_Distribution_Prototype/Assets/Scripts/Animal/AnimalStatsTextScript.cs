using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalStatsTextScript : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 animalPos = transform.parent.transform.parent.transform.position;
        Vector2 textPos = Camera.main.WorldToScreenPoint(animalPos);
        transform.position = textPos;

        GetComponent<Text>().fontSize = Mathf.Clamp((int)(100f / (float) _camera.orthographicSize), 0, 24);
    }
}
