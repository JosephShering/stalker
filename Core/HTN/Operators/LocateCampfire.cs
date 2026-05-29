using Godot;

[Tool]
[GlobalClass]
public partial class LocateCampfire : Operator
{
    public override OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: LocateCampfire";
    }
}
