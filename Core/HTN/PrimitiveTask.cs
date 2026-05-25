using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class PrimitiveTask : Task
{
    [Export]
    public Dictionary<string, bool> Preconditions = [];

    [Export]
    private Dictionary<string, bool> Effects = [];

    [Export]
    private Operator? Operator;

    public Dictionary<string, bool> GetEffects() => Effects;
    public Dictionary<string, bool> GetPreconditions() => Preconditions;

    public bool ArePreconditionsMet(HTNBlackboard blackboard)
    {
        foreach (var (key, value) in Preconditions)
        {
            var exists = blackboard.Data.TryGetValue(key, out bool bbValue);
            if (!exists && value)
            {
                // GD.PushError($"{key} not found in dictionary {blackboard}");
                return false;
            }

            if (bbValue != value) return false;
        }

        return true;
    }

    public void ApplyEffects(HTNBlackboard blackboard)
    {
        foreach (var (key, value) in Effects)
        {
            blackboard.Data[key] = value;
        }
    }

    public Operator? GetOperator() => Operator;

    public override string ToString() => $"PrimitiveTask: {ResourcePath.GetBaseName()}";
}
