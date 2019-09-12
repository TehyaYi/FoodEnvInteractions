using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodSource
{
    public abstract FoodSourceType Type { get; }
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

// Types of food sources
public enum FoodSourceType { Space_Maple, Fruit_Tree, Arid_Bush, Leafy_Bush }

// Names of the tiles that represent a food source
struct FoodSourceTileNames
{
    public static readonly string SPACE_MAPLE_TILE_NAME = "Space_Maple_Good_Top_Left";
    public static readonly string FRUIT_TREE_TILE_NAME = "Fruit_Tree_Good_Top_Left";
    public static readonly string ARID_BUSH_TILE_NAME = "Arid_Bush";
}

