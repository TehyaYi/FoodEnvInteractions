using System.Collections;
using System.Collections.Generic;

public class FoodSource
{
    public readonly string Name;
    public readonly int BaseOutput;
    public readonly FoodSourceType Type;

    private float _output;
    public float Output { get; set; }

    protected FoodSource(string name, int baseOutput, FoodSourceType foodSourceType)
    {
        this.Name = name;
        this.BaseOutput = baseOutput;
        this.Type = foodSourceType;

        // TODO: Hook up to Food-Environment Interaction API
        Output = BaseOutput;
    }

}

public enum FoodSourceType { Space_Maple, Fruit_Tree,  }

struct FoodSources
{
    public static readonly string SPACE_MAPLE_TILE_NAME = "Space_Maple_Good_Top_Left";
    public static readonly string FRUIT_TREE_TILE_NAME = "Fruit_Tree_Good_Top_Left";
}

