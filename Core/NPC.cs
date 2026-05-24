using Godot;


public partial class NPC : CharacterBody3D
{
    [Export]
    public float TimeToPeak = 0.5f;

    [Export]
    public float TimeToGround = 0.45f;

    [Export]
    public float JumpHeight = 1.1f;

    [Export]
    public Brain Brain = null!;

    [Export]
    public int ThoughtsPerMinute = 30;

    private double Timeout
    {
        get
        {
            return 1.0 / (60 / ThoughtsPerMinute);
        }
    }

    private double Time = 0.0;

    public override void _PhysicsProcess(double delta)
    {
        Time += delta;

        while (Time >= Timeout)
        {
            Time -= Timeout;
            var action = Brain.Run();
            GD.Print(action.ActionName);
        }
    }
}
