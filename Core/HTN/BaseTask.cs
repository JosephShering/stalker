using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public abstract partial class BaseTask : Resource
{
    [ExportGroup("Preconditions")]
    [Export]
    public Dictionary<StringName, bool> Preconditions = [];

    public bool IsPreconditionsMet(Dictionary<StringName, bool> data)
    {
        foreach (var (key, preconditionValue) in Preconditions)
        {
            if (data.TryGetValue(key, out bool value))
            {
                if (preconditionValue != value)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public abstract (Array<Operator>, Dictionary<StringName, bool>) Decompose(Dictionary<StringName, bool> blackboard);
    public virtual void ApplyEffect(Dictionary<StringName, bool> blackboard) { }
}
