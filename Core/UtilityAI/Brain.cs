using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Brain : Node
{
    [Export]
    public Blackboard blackboard = null!;

    [Export]
    public Array<Action> Actions = [];

    public List<(Action action, double score)> Window = [];

    public Action? action;

    public Action Run()
    {
        var allScores = Actions.Select((action) => (action, score: action.GetValue(blackboard)));
        Window = allScores.ToList();

        var highestScore = allScores.MaxBy((t) => t.score);

        action = highestScore.action;

        return action;
    }
}
