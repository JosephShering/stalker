using Godot;

[GlobalClass]
public partial class NPC : CharacterBody3D
{
    [Export]
    public float MoveSpeed = 1.1f;

    [Export]
    public float TimeToPeak = 0.5f;

    [Export]
    public float TimeToGround = 0.45f;

    [Export]
    public float JumpHeight = 1.1f;

    [Export]
    public HTNBlackboard Blackboard;

    public override void _PhysicsProcess(double delta)
    {

    }

    public void Fall()
    {
        var velocity = Velocity;
        var timeTo = velocity.Y <= 0 ? TimeToGround : TimeToPeak;

        velocity.Y -= (2.0f * JumpHeight / (timeTo * timeTo)) * (float)GetPhysicsProcessDeltaTime();

        Velocity = velocity;
    }
}
