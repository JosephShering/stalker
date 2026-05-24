using Godot;

[Tool]
[GlobalClass]
public partial class Point : Node3D
{

    public override void _PhysicsProcess(double delta)
    {
        if (!Engine.IsEditorHint()) return;

        DebugDraw3D.DrawSphere(GlobalPosition, 0.255555f, Colors.Crimson);
    }
}
