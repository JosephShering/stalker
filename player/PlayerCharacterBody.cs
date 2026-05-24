using System;
using Godot;


[GlobalClass]
public partial class PlayerCharacterBody : CharacterBody3D
{
    public event EventHandler OnLand;
    public event EventHandler OnJump;

    [Export]
    private double MoveSpeed = 4;

    [Export]
    private double Acceleration = 10;

    [Export]
    private double AirAcceleration = 1;

    [Export]
    private double AirFriction = 1;

    [Export]
    private double JumpHeight = 1.1;

    [Export]
    private double TimeToPeak = 0.5;

    [Export]
    private double TimeToGround = 0.5;

    [Export]
    private double StepHeight = 0.25;

    [Export]
    private double MaxFloorAngle = 45;

    [ExportGroup("Node Dependencies")]

    [Export]
    private CollisionShape3D CollisionShape;

    [Export]
    private Node3D Gimbal = null!;

    [Export]
    private Node3D Camera = null!;

    [Export]
    private RayCast3D BelowRaycast = null!;

    [Export]
    private RayCast3D AheadRaycast = null!;

    private double TimeTo
    {
        get
        {
            return Velocity.Y > 0 ? TimeToPeak : TimeToGround;
        }
    }

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

        // WatchFalling();
        // WatchLanding();
    }

    public void Jump()
    {
        var velocity = Velocity;

        velocity.Y = 2.0f * (float)JumpHeight / (float)TimeToPeak;

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
        var timeTo = (float)TimeTo;
        var fdelta = (float)delta;

        velocity.Y -= (2.0f * (float)JumpHeight / (timeTo * timeTo) * fdelta);
        Velocity = velocity;
    }

    private void UpdateVelocity(double delta)
    {
        var currentVelocity = new Vector2(Velocity.X, Velocity.Z);

        var acceleration = IsOnFloor() ? Acceleration : AirAcceleration;

        var nextVelocity = currentVelocity.MoveToward(WishVelocity, (float)(delta * acceleration));

        var velocity = Velocity;
        velocity.X = nextVelocity.X;
        velocity.Z = nextVelocity.Y;
        Velocity = velocity;
    }

    // private void WatchLanding()
    // {
    //     var justLanded = IsOnFloor() && !LastIsOnFloor;
    //     if (!justLanded) return;

    //     OnLand.Invoke(this, EventArgs.Empty);

    //     var query = new PhysicsRayQueryParameters3D()
    //     {
    //         From = GlobalPosition,
    //         To = GlobalPosition + new Vector3(0, -0.2f, 0)
    //     };

    //     var result = GetWorld3D().DirectSpaceState.IntersectRay(query);
    //     if (result.Count == 0) return;

    //     var collider = (GodotObject)result["collider"];
    //     GD.Print(collider);
    //     //TODO determine material for sound effect to play on land

    //     //TODO determine fall height to take damage and play ouch sound.
    // }

    // private void WatchFalling()
    // {
    //     var justTookOff = !IsOnFloor() && LastIsOnFloor;
    //     if (!justTookOff) return;

    //     OnJump.Invoke(this, EventArgs.Empty);

    //     WorldJumpHeight = GlobalPosition.Y;
    // }

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

    private void SetupStepRayCasts()
    {
        var stepHeight = (float)StepHeight;

        BelowRaycast.TargetPosition = new Vector3(0, -stepHeight, 0);

        AheadRaycast.TargetPosition = new Vector3(0, -stepHeight, 0);

        var shape = (CapsuleShape3D)CollisionShape.Shape;
        var radius = shape.Radius;
        var margin = 0.01f;
        AheadRaycast.Position = new Vector3(0, stepHeight + margin, -radius + margin);
    }

    private void HandleStep()
    {
        var stepHeight = (float)StepHeight;
        var rid = GetRid();
        var motion = GlobalTransform;
        var origin = motion.Origin;
        var motionParams = new PhysicsTestMotionParameters3D()
        {
            From = GlobalTransform,
            Motion = new Vector3(0, -0.5f, 0)
        };

        PhysicsServer3D.BodyTestMotion(rid, motionParams);
    }

    private bool IsSurfaceTooSteep(Vector3 normal)
    {
        return normal.AngleTo(Vector3.Up) > FloorMaxAngle;
    }

}
