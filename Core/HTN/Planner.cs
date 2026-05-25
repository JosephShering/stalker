using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Planner : Node
{
    [Export]
    public required CompoundTask RootTask;

    [Export]
    public required HTNBlackboard Blackboard;

    [Export]
    public int ThoughtsPerMinute = 30;

    private double Timeout
    {
        get
        {
            return 60.0 / ThoughtsPerMinute;
        }
    }

    private double Time = 0.0;

    public override void _PhysicsProcess(double delta)
    {
        Time += delta;

        while (Time > Timeout)
        {
            Time -= Timeout;

            var madePlan = RootTask.Run(Blackboard, out Array<Operator> operators);
            if (!madePlan) continue;

            GD.Print(operators);
        }
    }
}
