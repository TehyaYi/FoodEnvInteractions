using System.Collections;
using System.Collections.Generic;

// Need<T> holder class. Allows different types of Needs to be stored in one type for them to be put into collections.
// Implements behavior and properties all the types of needs should have.
public abstract class Need
{
    public abstract NeedType Type { get; }
    public abstract string Name { get; }
    private NeedCondition _currentCondition;
    public NeedCondition CurrentCondition { get { return _currentCondition; } protected set { _currentCondition = value; } }

    protected Need() { }
}

// Template Need class.
public abstract class Need<T> : Need
{
    public abstract SortedDictionary<T, NeedCondition> NeedConditions { get; }

    private T _currentValue;
    // When _currentValue changes, the current condition of the need should be updated.
    public T CurrentValue
    {
        get { return _currentValue; }
        set
        {
            _currentValue = value;
            UpdateCurrentCondition(value);
        }
    }

    protected Need() { }

    // The condition of a need should only ever be updated based on the value of the need.
    protected abstract void UpdateCurrentCondition(T value);
}

public enum NeedCondition { Bad, Neutral, Good }

public enum NeedType { Arid_Bush, Berry_Bush, Berry_Tree, Fruit_Tree, Leaf_Tree, Leafy_Bush, Space_Maple, Tallgrass, Gas_X, Gas_Y, Gas_Z, Temperature, Light }