using Godot;

[GlobalClass]
public partial class BrainRunner : Node
{
    public delegate void BrainActionDelegate(BrainAction? BrainAction);
    public event BrainActionDelegate? ActionChanged;

    [Export]
    public Brain Brain = null!;

    [Export]
    public int ThoughtsPerMinute = 60;

    public BrainAction? ChosenAction;

    private double Timeout { get => 60 / ThoughtsPerMinute; }

    private double Time = 0.0;

    public override void _PhysicsProcess(double delta)
    {
        Time += delta;

        while (Time > Timeout)
        {
            Time -= Timeout;
            var action = Brain.Run();

            if (ChosenAction?.ActionName != action?.ActionName)
            {
                ActionChanged?.Invoke(action);
            }

            ChosenAction = action;
            GD.Print(ChosenAction);
        }

    }
}
