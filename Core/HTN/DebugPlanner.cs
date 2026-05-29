using Godot;

[Tool]
[GlobalClass]
public partial class DebugPlanner : Node
{
    [Export]
    public Selector Selector = null!;

    [Export]
    public HTNBlackboard Blackboard = null!;

    [ExportToolButton("Test HTN", Icon = "CharacterBody2D")]
    public Callable ClickMeButton => Callable.From(Test);

    public void Test()
    {
        var (operators, _blackboard) = Selector.Decompose(Blackboard.Data);
        GD.Print(operators);
    }
}
