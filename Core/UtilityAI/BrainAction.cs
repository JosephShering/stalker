using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class BrainAction : Resource
{
    [Export]
    public string ActionName = "";

    [Export]
    public Array<Consideration> Considerations = [];

    public double GetValue(UtilityBlackboard blackboard)
    {
        var product = Considerations.Aggregate(1.0, (acc, c) => acc * c.GetValue(blackboard));

        var considerationCount = Considerations.Count;
        var modFactor = 1.0 - (1.0 / (double)considerationCount);
        return product + ((1.0 - product) * modFactor * product);
    }
}
