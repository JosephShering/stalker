using Godot;


[GlobalClass]
public partial class PlayerCharacterBody : CharacterBody3D
{
    [Export]
    private double MoveSpeed = 2.0;

    [Export]
    private double Acceleration = 5.0;

    [Export]
    private double Friction = 15.0;

    [Export]
    private double AirAcceleration = 1.0;

    [Export]
    private double AirFriction = 0.5;

    [Export]
    private double JumpHeight = 1.1;

    [Export]
    private double TimeToPeak = 0.5;

    [Export]
    private double TimeToGround = 0.4;

    [ExportGroup("Node Dependencies")]

    [Export]
    private Node3D Gimbal = null!;

    [Export]
    private Node3D Camera = null!;

    [Export]
    private RayCast3D BelowRaycast = null!;

    [Export]
    private RayCast3D AheadRaycast = null!;

    private Vector2 InputDirection = Vector2.Zero;
    private Vector2 WorldInputDirection = Vector2.Zero;
    private Vector2 WishVelocity = Vector2.Zero;
    private bool LastIsOnFloor = false;

    private double WorldJumpHeight = 0;

    private Vector2 MouseMovement = Vector2.Zero;

    public override void _Ready()
    {
        LastIsOnFloor = IsOnFloor();
        WorldJumpHeight = GlobalPosition.Y;

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            MouseMovement = mouseMotion.Relative * new Vector2(0.03f, 0.03f);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateInputDirection();
        CalculateWishVelocity();
        Fall(delta);
        UpdateVelocity(delta);
        RotateCamera();

        MoveAndSlide();
    }

    public void Jump()
    {
        var velocity = Velocity;

        velocity.Y = (float)(2.0 * JumpHeight / TimeToPeak);

        Velocity = velocity;
    }

    private void UpdateInputDirection()
    {
        InputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

        var worldInputDirection3D = Transform.Basis * new Vector3(InputDirection.X, 0, InputDirection.Y);
        WorldInputDirection = new Vector2(worldInputDirection3D.X, worldInputDirection3D.Z);
    }

    private void CalculateWishVelocity()
    {
        var nextWishVelocity = (float)MoveSpeed * WorldInputDirection;
        WishVelocity = new Vector2(nextWishVelocity.X, nextWishVelocity.Y);
    }

    private void Fall(double delta)
    {
        if (IsOnFloor()) return;

        var velocity = Velocity;
        var timeTo = velocity.Y <= 0 ? TimeToGround : TimeToPeak;

        var fallSpeed = 2.0 * JumpHeight / (timeTo * timeTo);
        velocity.Y -= (float)(fallSpeed * delta);
        Velocity = velocity;
    }

    private void UpdateVelocity(double delta)
    {
        var currentVelocity = new Vector2(Velocity.X, Velocity.Z);
        var acceleration = 0.0;

        if (Mathf.IsZeroApprox(WishVelocity.Length()))
        {
            if (IsOnFloor())
                acceleration = Friction;
            else
                acceleration = AirFriction;
        }
        else
        {
            if (IsOnFloor())
                acceleration = Acceleration;
            else
                acceleration = AirAcceleration;
        }

        var blendValue = (float)(delta * acceleration);
        var nextVelocity = currentVelocity.MoveToward(WishVelocity, blendValue);

        var velocity = Velocity;
        velocity.X = nextVelocity.X;
        velocity.Z = nextVelocity.Y;
        Velocity = velocity;
    }

    private void RotateCamera()
    {
        var horizontalMovement = MouseMovement.X;
        var isZero = Mathf.IsZeroApprox(horizontalMovement);

        if (!isZero)
            Gimbal.RotateY(-horizontalMovement);


        var verticalMovement = MouseMovement.Y;
        isZero = Mathf.IsZeroApprox(verticalMovement);

        if (!isZero)
            Camera.RotateX(-verticalMovement);

        var deg = Camera.RotationDegrees;
        deg.X = Mathf.Clamp(deg.X, -89, 89);

        Camera.RotationDegrees = deg;

        MouseMovement = Vector2.Zero;
    }

}
