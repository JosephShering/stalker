using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class ReserveCampfireSpot : Operator
{
    private Campfire campfire = null!;

    public override OperatorResponse Enter(Dictionary<StringName, bool> Data)
    {
        var campfire = CampfireRegistry.Instance.GetNearestOpen(Npc.GlobalPosition);
        if (campfire == null)
        {
            return OperatorResponse.Failure;
        }

        this.campfire = campfire;
        campfire.TakeFirstAvailableSeat();
        return OperatorResponse.Success;
    }

    public override string ToString()
    {
        return $"Operator: ReserveCampfireSpot";
    }
}
