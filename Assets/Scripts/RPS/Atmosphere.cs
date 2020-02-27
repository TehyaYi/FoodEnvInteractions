using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple atmosphere class, not used yet.
//TODO to be expanded.
public class Atmosphere
{
    float gasx, gasy, gasz, temp;

    public Atmosphere() {
        gasx = gasy = gasz = temp = 0;
    }

    public Atmosphere(float x, float y, float z, float t) {
        gasx = x; gasy = y; gasz = z; temp = t;
    }
}
