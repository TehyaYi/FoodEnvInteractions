using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainRangeScriptableObject", order = 1)]
public class TerrainRangeScriptableObject : RangeScriptableObject
{
	[SerializeField] TerrainEnum tiles;
    [SerializeField] int[] values;
}
