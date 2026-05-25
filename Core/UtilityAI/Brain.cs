using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Brain : Node
{
    [Export]
    public UtilityBlackboard blackboard = null!;

    [Export]
    public Array<BrainAction> Actions = [];

    public List<(BrainAction action, double score)> Window = [];

    public BrainAction? action;

    public BrainAction Run()
    {
        var allScores = Actions.Select((action) => (action, score: action.GetValue(blackboard)));
        Window = allScores.ToList();

        var highestScore = allScores.MaxBy((t) => t.score);

        action = highestScore.action;

        return action;
    }
}
