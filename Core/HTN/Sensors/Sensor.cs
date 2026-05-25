using Godot;


[GlobalClass]
public abstract partial class Sensor : Node
{
    [Export]
    public required HTNBlackboard Blackboard;

    [Export]
    public required string Key;

    public override void _PhysicsProcess(double delta)
    {
        var result = Evaluate();
        Blackboard.Data[Key] = result;
    }

    protected abstract bool Evaluate();
}
