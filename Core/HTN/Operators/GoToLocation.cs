using Godot;

[Tool]
[GlobalClass]
public partial class GoToLocation : Operator
{
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: GoToLocation";
    }
}
