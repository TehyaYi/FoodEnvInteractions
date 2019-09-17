using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Float based Need class. 
/// <para>Condition ranges are based on float number limits sorted in ascending order.</para>
/// </summary>
public class NeedF : Need<float>
{
    private readonly NeedType _type;
    private readonly string _name;
    private readonly SortedDictionary<float, NeedCondition> _needConditions;

    public NeedF(NeedType type, string name, SortedDictionary<float, NeedCondition> needConditions)
    {
        this._type = type;
        this._name = name;
        _needConditions = needConditions;
    }

    public override NeedType Type { get { return _type; } }
    public override string Name { get { return _name; } }

    /// <summary>
    /// Returns the sorted mapping of needs condition limits and associated needs conditions
    /// </summary>
    public override SortedDictionary<float, NeedCondition> NeedConditions { get { return _needConditions; } }

    /// <summary>
    /// Finds where the value lies in the condition ranges and sets the new condition.
    /// </summary>
    /// <param name="value">The new value of the need</param>
    protected override void UpdateCurrentCondition(float value)
    {
        foreach (KeyValuePair<float, NeedCondition> keyValuePair in NeedConditions)
        {
            if (value < keyValuePair.Key)
            {
                CurrentCondition = keyValuePair.Value;
                return;
            }
        }
    }
}
