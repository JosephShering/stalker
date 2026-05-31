using Godot;

[GlobalClass]
public partial class Item : Resource
{
    [Export]
    public string DisplayName = "No Name";

    [Export]
    public Vector2I Size = new(1, 1);

    [Export]
    public int StackLimit = 1;

    [Export]
    public Texture2D Image = null!;

    [Export]
    public string[] Effects = [];
}
