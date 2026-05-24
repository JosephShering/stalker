using Godot;

[Tool]
[GlobalClass]
public partial class CampfireSeat : Node3D
{
    public bool IsTaken = false;

    public override void _PhysicsProcess(double delta)
    {
        if (!Engine.IsEditorHint()) return;

        DebugDraw3D.DrawPosition(GlobalTransform, Colors.Crimson);
        // DebugDraw3D.DrawSphere(GlobalPosition, 0.25f, Colors.Crimson);

        DebugDraw3D.DrawText(GlobalPosition + new Vector3(0, 0.5f, 0), Name, 28);
    }
}
