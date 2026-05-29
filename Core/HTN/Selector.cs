using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Selector : BaseTask
{
    [Export]
    public Sequence[] Options = [];

    public override (Array<Operator>, Dictionary<StringName, bool>) Decompose(Dictionary<StringName, bool> data)
    {
        foreach (var option in Options)
        {
            if (!option.IsValid(data)) continue;

            var newBlackboard = data.Duplicate(true)!;

            var (operators, newData) = option.Decompose(data.Duplicate(true)!);
            if (operators.Count == 0) continue;

            return (operators, newData);
        }

        return ([], data);
    }
}
