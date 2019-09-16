using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStatsTextScript : MonoBehaviour
{
    void Update()
    {
        Vector2 animalPos = transform.parent.transform.parent.transform.position;
        Vector2 textPos = Camera.main.WorldToScreenPoint(animalPos);
        transform.position = textPos;
    }
}
