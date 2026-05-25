using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class CompoundTaskMethod : Resource
{
    [Export]
    public Dictionary<string, bool> Preconditions = [];

    [Export]
    public Array<Task> Tasks = [];

    private Expression ValidationExpression = null!;

    public bool IsValid(HTNBlackboard blackboard)
    {
        foreach (var (key, value) in Preconditions)
        {
            var exists = blackboard.Data.TryGetValue(key, out bool bbValue);
            if (!exists) return false;
            if (bbValue != value) return false;
        }

        return true;
    }

    public bool Run(HTNBlackboard blackboard, out Array<Operator> operators)
    {
        Array<Operator> methodOperators = [];
        HTNBlackboard wsBlackboard = (HTNBlackboard)blackboard.DuplicateDeep();

        foreach (var task in Tasks)
        {
            if (task is PrimitiveTask primitiveTask)
            {
                var arePreconditionsMet = primitiveTask.ArePreconditionsMet(wsBlackboard);
                if (arePreconditionsMet)
                {
                    primitiveTask.ApplyEffects(wsBlackboard);
                    var op = primitiveTask.GetOperator();

                    if (op != null)
                        methodOperators.Add(op);
                    else
                        GD.PushError($"Operator returned from {primitiveTask} is null");
                }
                else
                {
                    // GD.Print(wsBlackboard);
                    // GD.Print($"Failed: {primitiveTask.ResourcePath.GetBaseName()}");
                    operators = [];
                    return false;
                }

            }
            else if (task is CompoundTask compoundTask)
            {
                var isSuccess = compoundTask.Run(wsBlackboard, out Array<Operator> compoundOperators);
                if (!isSuccess)
                {
                    // GD.Print($"Failed: {compoundTask.ResourcePath.GetBaseName()}");
                    operators = [];
                    return false;
                }

                methodOperators.AddRange(compoundOperators);
            }
        }

        operators = methodOperators;
        return true;
    }

    public override string ToString()
    {
        return $"CTM: {ResourcePath.GetBaseName()}";
    }
}
