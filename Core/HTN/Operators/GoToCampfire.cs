using Godot;

[Tool]
[GlobalClass]
public partial class GoToCampfire : Operator
{
    public override OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: SitAtCampfire";
    }
}
