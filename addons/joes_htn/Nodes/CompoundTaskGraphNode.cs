using Godot;

[GlobalClass]
public partial class CompoundTaskGraphNode : GraphNode
{

    public override void _Ready()
    {
        var label = new Label();
        label.Text = "Compound Task";

        GetTitlebarHBox().AddChild(label);

        SetSlot(
            slotIndex: 0,
            enableLeftPort: true,
            typeLeft: (int)SlotType.CompoundTask,
            colorLeft: CompoundTaskGraphEdit.CompoundTaskColor,
            enableRightPort: true,
            typeRight: (int)SlotType.CompoundTask,
            colorRight: CompoundTaskGraphEdit.CompoundTaskColor
        );
    }

}
