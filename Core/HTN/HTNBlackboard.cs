using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class HTNBlackboard : Resource
{
    [Export]
    public Dictionary<StringName, Variant> Data = [];

    public override string ToString()
    {
        var jsonData = Json.Stringify(Data);
        return $"HTNBlackboard {jsonData}";
    }

}
