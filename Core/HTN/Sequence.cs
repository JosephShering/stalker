using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Sequence : Resource
{
    [Export]
    public Dictionary<StringName, bool> Preconditions = null!;

    [Export]
    public BaseTask[] Tasks = [];

    public bool IsValid(Dictionary<StringName, bool> data)
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

    public (Array<Operator>, Dictionary<StringName, bool>) Decompose(Dictionary<StringName, bool> data)
    {
        Array<Operator> operators = [];
        Dictionary<StringName, bool> localData = data;

        foreach (var task in Tasks)
        {
            if (!task.IsPreconditionsMet(data))
            {
                return ([], data);
            }

            var (newOperators, newData) = task.Decompose(localData);
            if (newOperators.Count == 0) return ([], data);

            localData = newData;
            operators.AddRange(newOperators);
        }

        return (operators, localData);
    }
}
