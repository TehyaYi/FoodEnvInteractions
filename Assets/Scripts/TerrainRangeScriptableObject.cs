using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainRangeScriptableObject", order = 2)]
public class TerrainRangeScriptableObject : RangeScriptableObject
{
    //may add a enum or Dictionary<enum, int> in order to go
    //from enum to tileValues
    [SerializeField] float[] tileValues;//the values of each tile

    //tiles[] = what tile it is, should correspond to its value in tileValues[]
    public float getValue(int[] tiles)
    {
        float value = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            value += tileValues[tiles[i]];
        }
        return value;
    }
}
