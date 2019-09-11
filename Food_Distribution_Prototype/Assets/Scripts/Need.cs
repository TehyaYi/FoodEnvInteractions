using System.Collections;
using System.Collections.Generic;

public abstract class Need
{
    protected Need(string name)
    {
        Name = name;
    }

    public readonly string Name;
    private NeedCondition _currentCondition;
    public NeedCondition CurrentCondition { get { return _currentCondition; } protected set { _currentCondition = value; } }
}

public abstract class Need<T> : Need
{
    public readonly SortedDictionary<T, NeedCondition> NeedConditions;

    private T _currentValue;
    public T CurrentValue { get { return _currentValue; }
        set {
            _currentValue = value;
            UpdateCurrentCondition(value);
            }
    }

    protected Need(string name, SortedDictionary<T, NeedCondition> needCondition) : base(name)
    {
        this.NeedConditions = needCondition;
    }

    protected abstract void UpdateCurrentCondition(T value);
}


public enum NeedCondition { Bad, Neutral, Good }
