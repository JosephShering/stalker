using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Task : BaseTask
{
    [Export]
    public Operator Operator = null!;

    public override (Array<Operator>, Dictionary<StringName, bool>) Decompose(Dictionary<StringName, bool> data)
    {
        return ([Operator], data);
    }
}
