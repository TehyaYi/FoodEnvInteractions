using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReader
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

    public int[] ReadMap(Tilemap map, int x, int y, int radius){
    	int[] tiles = new int[radius*radius];
    	int index = 0;
    	for(int r = y-radius; r <= y+radius; r++){
    		for(int c = x-radius; c <= c+radius; c++){

    			if((y-r)*(y-r)+(x-c)*(x-c) <= radius*radius){//if within radius, x^2+y^2 <= r^2
    				//find out the what the tile is and add to tiles[]
    				Sprite spr = map.GetSprite(new Vector3Int(c, r, 0));
    				if(spr != null){
    					int val = getValue(spr);
    					if(val != -1){
	    					tiles[index] = val;
	    					index++;
	    				}
    				}
    			}

    		}
    	}
    	int[] final = new int[index];
    	for(int i = 0; i < index; i++){
    		final[i] = tiles[i];
    	}
    	return final;
    }
}
