using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Blackboard : Resource
{
    [Export]
    public Dictionary<string, float> Data = [];
}
