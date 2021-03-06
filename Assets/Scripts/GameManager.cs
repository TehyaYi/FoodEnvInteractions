﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//temporary game manager class for storing values
//should be a singleton
public class GameManager : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float gasX = 0;
    [Range(0f, 1f)] [SerializeField] float gasY = 0;
    [Range(0f, 1f)] [SerializeField] float gasZ = 0;
    [Range(0f, 100f)] [SerializeField] float temp = 0;
    Atmosphere atm;

    public float getGX() { return gasX; }
    public float getGY() { return gasY; }
    public float getGZ() { return gasZ; }
    public float getTemp() { return temp; }

    public void Start()
    {
        atm = new Atmosphere(gasX, gasY, gasZ, temp);
        
    }
}