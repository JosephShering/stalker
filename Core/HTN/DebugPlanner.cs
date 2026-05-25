using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class DebugPlanner : Node
{
    [Export]
    public CompoundTask RootTask = null!;

    [Export]
    public HTNBlackboard Blackboard = null!;

    [ExportToolButton("Test HTN", Icon = "CharacterBody2D")]
    public Callable ClickMeButton => Callable.From(Test);

    public void Test()
    {
        var isSuccess = RootTask.Run(Blackboard, out Array<Operator> operators);
        if (!isSuccess)
            GD.Print("Could not come up with plan");
        else
            GD.Print(operators);
    }
}
