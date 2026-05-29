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
    private Dictionary<StringName, bool> Data = [];
    private Node Agent = null!;

    public virtual void Enter() { }

    public virtual OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public virtual void Exit() { }


}
