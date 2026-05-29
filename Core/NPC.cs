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
    public HTNBlackboard Blackboard = null!;

    [ExportGroup("Stats")]
    [Export]
    public int Health = 100;

    [Export]
    public int Hunger = 100;

    [Export]
    public int Energy = 100;

    public override void _PhysicsProcess(double delta)
    {
        HTNBlackboard b = new();

        var data = Blackboard.Data;
        data["low_health"] = Health < 30;
        data["is_hungry"] = Hunger < 30;
        data["is_tired"] = Energy < 30;
        data["has_heal_item"] = false;
        data["at_campfire"] = false;
        data["campfire_position"] = false;
    }

    public void Fall()
    {
        var velocity = Velocity;
        var timeTo = velocity.Y <= 0 ? TimeToGround : TimeToPeak;

        velocity.Y -= (2.0f * JumpHeight / (timeTo * timeTo)) * (float)GetPhysicsProcessDeltaTime();

        Velocity = velocity;
    }
}
