using System.Collections;
using System.Collections.Generic;

public class NeedF : Need<float>
{
    public static readonly string name;
    private readonly SortedDictionary<float, NeedCondition> _needConditions;

    public NeedF(string name, SortedDictionary<float, NeedCondition> needConditions)
    {
        _needConditions = needConditions;
    }

    public override string Name { get { return name; } }

    public override SortedDictionary<float, NeedCondition> NeedConditions { get { return _needConditions; } }

    protected override void UpdateCurrentCondition(float value)
    {
        foreach(KeyValuePair<float, NeedCondition> keyValuePair in NeedConditions)
        {
            if(value < keyValuePair.Key)
            {
                CurrentCondition = keyValuePair.Value;
            }
        }
    }
}
