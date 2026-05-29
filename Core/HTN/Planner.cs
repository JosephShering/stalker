using System.Collections.Generic;
using Godot;
using Godot.Collections;
using KdlSharp;

[GlobalClass]
public partial class Planner : Node
{
    [Export]
    public required BaseTask RootTask;

    [Export]
    public required HTNBlackboard Blackboard;

    [Export]
    string File;

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

    public Array<Operator> Plan = [];

    // private void Parse(IList<KdlNode?> nodes)
    // {
    //     if (nodes.Count == 0) return;

    //     foreach (var child in nodes)
    //     {
    //         switch (child?.Name)
    //         {
    //             case "selector":
    //                 var selector = new Selector();
    //                 Parse(child.Children ?? []);
    //                 break;
    //             case "sequence":
    //                 var sequence = new Sequence();
    //                 var d = Parse(child.Children ?? []);
    //                 break;
    //             case "task":
    //                 var task = new Task();
    //                 break;
    //         }
    //     }
    // }

    public override void _Ready()
    {
        var file = FileAccess.Open(File, FileAccess.ModeFlags.Read);

        var document = KdlDocument.Parse(file.GetAsText());
        var rootSelector = document.Nodes[0];

        foreach (var child in rootSelector.Children)
        {
            GD.Print(child.Name);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Time += delta;

        while (Time > Timeout)
        {
            Time -= Timeout;

            var (operators, _) = RootTask.Decompose(Blackboard.Data);
            Plan = operators;

            GD.Print(operators);
        }
    }
}
