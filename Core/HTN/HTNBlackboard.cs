using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class HTNBlackboard : Resource
{
    [Export]
    public Dictionary<string, bool> Data = [];

    public override string ToString()
    {
        var jsonData = Json.Stringify(Data);
        return $"HTNBlackboard {jsonData}";
    }
}
