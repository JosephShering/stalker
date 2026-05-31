using Godot;
using Godot.Collections;

public enum OperatorResponse
{
    Success,
    Failure,
    Waiting
}

[Tool]
[GlobalClass]
public abstract partial class Operator : Resource
{
    protected Dictionary<StringName, bool> Data = [];
    protected NPC Npc = null!;

    public virtual OperatorResponse Enter(Dictionary<StringName, bool> Data)
    {
        return OperatorResponse.Success;
    }

    public virtual OperatorResponse Tick(Dictionary<StringName, bool> Data, double delta)
    {
        return OperatorResponse.Success;
    }

    public virtual void Exit(Dictionary<StringName, bool> Data) { }
}
