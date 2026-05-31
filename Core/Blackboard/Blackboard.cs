using Godot;
using Godot.Collections;

enum Conditional
{
    E,
    NE,
    LT,
    LTE,
    RT,
    RTE,
}

[GlobalClass]
public partial class Blackboard : Resource
{
    [Export]
    public Dictionary<StringName, Variant> Data = [];

    public bool Has(StringName key)
    {
        return Data.ContainsKey(key);
    }

    public bool TryGetBool(StringName key, out bool boolValue)
    {
        if (Data.TryGetValue(key, out Variant value))
        {
            boolValue = value.AsBool();
            return true;
        }

        boolValue = false;
        return false;
    }

    public bool TryGetDouble(StringName key, out double value)
    {
        if (Data.TryGetValue(key, out Variant v))
        {
            value = v.AsDouble();
            return true;
        }

        value = 0.0;
        return false;
    }

    public bool TryGetVector3(StringName key, out Vector3 value)
    {
        if (Data.TryGetValue(key, out Variant v))
        {
            value = v.AsVector3();
            return true;
        }

        value = Vector3.Zero;
        return false;
    }
}
