using Godot;

[GlobalClass]
public partial class PrimitiveTaskGraphNode : GraphNode
{
    public override void _Ready()
    {
        var label = new Label();
        label.Text = "Primitive Task";

        GetTitlebarHBox().AddChild(label);

        SetSlot(
            slotIndex: 0,
            enableLeftPort: true,
            typeLeft: (int)SlotType.PrimitiveTask,
            colorLeft: CompoundTaskGraphEdit.PrimitiveTaskColor,
            enableRightPort: true,
            typeRight: (int)SlotType.PrimitiveTask,
            colorRight: CompoundTaskGraphEdit.PrimitiveTaskColor
        );
    }

}
