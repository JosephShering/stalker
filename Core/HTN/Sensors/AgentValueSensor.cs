using Godot;

[GlobalClass]
public partial class ValueSensor : Sensor
{
    [Export]
    public required Node3D Agent;

    [Export]
    public required string AgentKey;

    [Export]
    public required bool Negate;

    protected override bool Evaluate()
    {
        var result = Agent.Get(AgentKey).AsBool();
        return Negate ? !result : result;
    }
}
