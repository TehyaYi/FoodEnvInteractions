using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [Deprecated] A class that represents the closed area that a population lives on
/// </summary>
public class EnclosedArea : Area
{
    Atmosphere internalAtmosphere;

    //constructors
    public EnclosedArea() : base() {}
    public EnclosedArea(List<Vector3Int> field) : base(field){
        internalAtmosphere = new Atmosphere();
    }
    public EnclosedArea(List<Vector3Int> field, float gasx, float gasy, float gasz, float temp) : base(field) {
        internalAtmosphere = new Atmosphere(gasx,gasy,gasz,temp);
    }
    public EnclosedArea(List<Vector3Int> field, Atmosphere atm) : base(field)
    {
        internalAtmosphere = atm;
    }

    //override to return internal atmosphere
    public override Atmosphere GetAtmosphere() {
        return internalAtmosphere;
    }
}
