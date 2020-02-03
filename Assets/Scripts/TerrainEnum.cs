using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainEnum", order = 2)]
public class TerrainEnum : ScriptableObject
{
	[SerializeField] Sprite[] tiles;
	public Sprite[] Tiles { get; }
    [SerializeField] int[] values;
	public int[] Values { get; }
	
	public int getValue(Sprite spr){
		for(int i = 0; i < tiles.Length; i++)
			if(tiles[i] == spr)
				return Values[i];
		return -1;
	}
}
