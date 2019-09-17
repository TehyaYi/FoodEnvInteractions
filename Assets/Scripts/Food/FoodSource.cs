using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodSource
{
    public abstract NeedType Type { get; }
    public abstract string Name { get; }
    public abstract int BaseOutput { get; }

    public readonly Vector2 Position;

    private float _output;
    public float Output { get; set; }

    protected FoodSource(float output)
    {
        this.Output = output;
    }

}

// Names of the tiles that represent a food source
struct FoodSourceTileNames
{
    public static readonly string SPACE_MAPLE_TILE_NAME = "Space_Maple_Good_Bot_Left";
    public static readonly string FRUIT_TREE_TILE_NAME = "Fruit_Tree_Good_Bot_Left";
    public static readonly string LEAF_TREE_TILE_NAME = "Leaf_Tree_Good_Bot_Left";
    public static readonly string BERRY_TREE_TILE_NAME = "Berry_Tree_Good_Bot_Left";
    public static readonly string TALL_GRASS_TILE_NAME = "Tall_Grass_Good";
    public static readonly string BERRY_BUSH_TILE_NAME = "Berry_Bush_Good";
    public static readonly string ARID_BUSH_TILE_NAME = "Arid_Bush_Good";
    public static readonly string LEAFY_BUSH_TILE_NAME = "Leafy_Bush_Good_Left";
}

