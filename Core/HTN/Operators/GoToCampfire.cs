using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class GoToCampfire : Operator
{
    public override OperatorResponse Tick(Dictionary<StringName, bool> Data, double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: SitAtCampfire";
    }
}
