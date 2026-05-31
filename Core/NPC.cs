using GameReadyHtn;
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
    public int Energy = 19;

    [Export]
    public int ThreatLevel = 49;

    [Export]
    public Inventory Inventory = null!;

    [ExportGroup("Nodes")]
    [Export]
    private NavigationAgent3D Agent;

    public override void _Ready()
    {
        HtnAgent Agent = new()
        {
            States = new()
            {
                ["Health"] = Health,
                ["Hunger"] = Hunger,
                ["Energy"] = Energy,
                ["ThreatLevel"] = ThreatLevel
            },
            Task = new HtnSelectorTask("Root")
            {
                Tasks = [
                    new HtnSelectorTask("Rest") {
                        Requirements = [
                            new HtnCondition() {
                                State = "Energy",
                                Comparison = HtnComparison.LessThan,
                                Value = 20,
                            },
                            new HtnCondition() {
                                State = "ThreatLevel",
                                Comparison = HtnComparison.LessThan,
                                Value = 50
                            }
                        ],
                        Tasks = [
                            new HtnPrimitiveTask("Sleep") {
                                Effects = [
                                    new HtnEffect() {
                                        State = "Energy",
                                        Value = 10,
                                        Operation = HtnOperation.IncreaseBy
                                    }
                                ]
                            },
                        ]
                    },
                ]
            }
        };

        var plan = Agent.FindPlan();
        if (plan == null)
        {
            GD.Print("No plan");
        }

        foreach (var a in plan.Tasks)
        {
            GD.Print(a.Name);
        }
    }

    public void Fall()
    {
        var velocity = Velocity;
        var timeTo = velocity.Y <= 0 ? TimeToGround : TimeToPeak;

        velocity.Y -= (2.0f * JumpHeight / (timeTo * timeTo)) * (float)GetPhysicsProcessDeltaTime();

        Velocity = velocity;
    }

    public void SetNavigateTo(Vector3 TargetPosition)
    {
        Agent.TargetPosition = TargetPosition;
    }

    public void Navigate()
    {
        //move toward position
    }

    public void UseItem()
    {
        //Play animation


    }
}
