using System;
using System.Collections.Generic;
using UnityEngine;

//temporary enum before merging with 
public enum Tiles { Rock, Sand, Dirt, Grass };

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainNeedRangeScriptableObject", order = 2)]
public class TerrainNeedScriptableObject : NeedScriptableObject
{

    //Thanks to Helical at https://answers.unity.com/questions/642431/dictionary-in-inspector.html
    //custom dictionary visible in inspector
    [Serializable]
    public struct Dict
    {
        public Tiles type;
        public int value; //value of tile
    }
    [SerializeField] private Dict[] tileVal = new Dict[4];

    //Workaround: dictionary to be initialized
    private Dictionary<Tiles, int> tileDic;

    //Gets called when value of scriptable object changes in the inspector
    private void OnValidate()
    {
        //initialize the dictionary
        tileDic = new Dictionary<Tiles, int>();
        for(int i = 0; i < tileVal.Length; i++)
        {
            tileDic.Add(tileVal[i].type, tileVal[i].value);
        }
    }

    public float getValue(Tiles tile)
    {
        try{ //go in dictionary to retrieve value of tile
            return tileDic[tile];
        }catch (KeyNotFoundException){
            //tiles is not contained in tileValue
            return 0;
        }
    }

    public float getValue(Tiles[] tiles)
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
