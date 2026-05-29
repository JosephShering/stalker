using Godot;
using Godot.Collections;

[GlobalClass]
public partial class UtilityBlackboard : Resource
{
    [Export]
    public Dictionary<string, double> Data = [];
}
