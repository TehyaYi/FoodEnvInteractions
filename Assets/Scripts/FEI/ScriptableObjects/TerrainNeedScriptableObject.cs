using System;
using System.Collections.Generic;
using UnityEngine;

//temporary enum before merging
public enum TileType { Rock, Sand, Dirt, Grass, Liquid };

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainNeedRangeScriptableObject", order = 2)]
public class TerrainNeedScriptableObject : NeedScriptableObject
{
    //Thanks to Helical at https://answers.unity.com/questions/642431/dictionary-in-inspector.html
    //custom dictionary visible in inspector
    [Serializable]
    public struct Dict
    {
        public TileType type;
        public int value; //value of tile
    }
    [SerializeField] private Dict[] tileVal = new Dict[4];

    //Workaround: dictionary to be initialized
    private Dictionary<TileType, int> tileDic;

    //Gets called when value of scriptable object changes in the inspector
    public void OnValidate()
    {
        base.OnValidate();
        //initialize the dictionary
        tileDic = new Dictionary<TileType, int>();
        for(int i = 0; i < tileVal.Length; i++)
        {
            tileDic.Add(tileVal[i].type, tileVal[i].value);
        }
    }

    public float getValue(TileType tile)
    {
        try{ //go in dictionary to retrieve value of tile
            return tileDic[tile];
        }catch (KeyNotFoundException){
            //tiles is not contained in tileValue
            return 0;
        }
    }

    public float getValue(TileType[] tiles)
    {
        float value = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            //use getValue(Tiles tile) and sum them
            value += getValue(tiles[i]);
        }
        return value;
    }
}
