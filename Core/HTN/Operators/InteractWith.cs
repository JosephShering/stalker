using Godot;

[Tool]
[GlobalClass]
public partial class InteractWith : Operator
{
    public override OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: InteractWith";
    }
}
