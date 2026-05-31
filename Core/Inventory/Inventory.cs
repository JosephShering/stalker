using Godot;
using System.Linq;

[GlobalClass]
public partial class Inventory : Resource
{
    [Export]
    public Vector2 Size;

    public InventorySlot[] Slots = [];

    public void AddItemSlot(InventorySlot slot)
    {
        //Find room for it
    }

    public bool HasItemWithEffect(string effect)
    {
        foreach (var slot in Slots)
        {
            if (slot.Item.Effects.Any(e => e == effect))
            {
                return true;
            }
        }


        return false;
    }
}
