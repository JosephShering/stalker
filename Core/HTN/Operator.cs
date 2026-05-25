using Godot;

[Tool]
[GlobalClass]
public partial class Operator : Resource
{
    [Export]
    public string Command = "";

    public override string ToString()
    {
        return $"Operator: {Command}";
    }
}
