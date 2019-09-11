using System.Collections;
using System.Collections.Generic;

public class NeedF : Need<float>
{
    public static readonly string name;

    public NeedF(string name, SortedDictionary<float, NeedCondition> needConditions) : base(name, needConditions)
    {

    }

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
