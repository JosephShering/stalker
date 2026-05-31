using Godot;

[GlobalClass]
public partial class InventorySlot : Resource
{
    [Export]
    public Item Item { get; protected set; } = null!;

    [Export]
    public int Stacks { get; protected set; } = 0;

    [Export]
    public Vector2I Position = new Vector2I(0, 0);

    Rect2I Dimensions { get => new Rect2I(Position.X, Position.Y, Item.Size.X, Item.Size.Y); }

    void RotateRight()
    {
    }

    void RotateLeft()
    {
    }
}
