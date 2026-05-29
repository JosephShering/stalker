using Godot;

[Tool]
[GlobalClass]
public partial class ReserveCampfireSpot : Operator
{
    public override OperatorResponse Tick(double delta)
    {
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: ReserveCampfireSpot";
    }
}
