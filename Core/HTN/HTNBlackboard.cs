using Godot;

[Tool]
[GlobalClass]
public partial class HTNBlackboard : Resource
{
    [Export]
    public Godot.Collections.Dictionary<StringName, bool> Data = [];

    public override string ToString()
    {
        var jsonData = Json.Stringify(Data);
        return $"HTNBlackboard {jsonData}";
    }

}
