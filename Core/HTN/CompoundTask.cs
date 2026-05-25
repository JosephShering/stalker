using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class CompoundTask : Task
{
    [Export]
    public Array<CompoundTaskMethod> Methods = [];

    public bool Run(HTNBlackboard blackboard, out Array<Operator> operators)
    {
        foreach (var method in Methods)
        {
            var isValid = method.IsValid(blackboard);
            if (!isValid) continue;

            var isSuccess = method.Run(blackboard, out Array<Operator> childOperators);
            if (isSuccess)
            {
                operators = childOperators;
                return true;
            }
        }

        operators = [];
        return false;
    }
}
