using Godot;

[GlobalClass]
public partial class Consideration : Resource
{
    [Export]
    public string BlackboardKey = "";

    [Export]
    public Curve Curve = null!;

    [Export]
    public double Highend = 1.0;

    public double GetValue(UtilityBlackboard blackboard)
    {
        var exists = blackboard.Data.TryGetValue(BlackboardKey, out double value);
        if (!exists)
        {
            GD.PushWarning($"{BlackboardKey} not in Blackboard...setting it");
            blackboard.Data.Add(BlackboardKey, 0.0f);
            return 0.0f;
        }

        var sample = Curve.SampleBaked((float)(value / Highend));

        return Mathf.Clamp(sample, 0.0, 1.0);
    }

    public override string ToString() => $"Consideration: {ResourcePath.GetBaseName()}";
}
