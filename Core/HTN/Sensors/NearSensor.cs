using Godot;

[GlobalClass]
public partial class NearSensor : Sensor
{
    [Export]
    public required Node3D Parent;

    [Export]
    public required Node3D Target;

    [Export]
    public required double DistanceThreshold = 10.0;

    protected override bool Evaluate()
    {
        return Parent.GlobalPosition.DistanceTo(Target.GlobalPosition) <= DistanceThreshold;
    }
}
