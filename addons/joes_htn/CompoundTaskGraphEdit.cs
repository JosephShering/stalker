using Godot;

enum SlotType
{
    CompoundTask = 0,
    PrimitiveTask
}

[Tool]
[GlobalClass]
public partial class CompoundTaskGraphEdit : GraphEdit
{
    public static Color CompoundTaskColor = Colors.Pink;
    public static Color PrimitiveTaskColor = Colors.PeachPuff;

    private ActionMenu ActionMenu = null!;
    private Vector2 LastClickPosition;

    public override void _Ready()
    {
        GD.Print("In tree");
        AddValidConnectionType((int)SlotType.CompoundTask, (int)SlotType.PrimitiveTask);
        AddValidConnectionType((int)SlotType.CompoundTask, (int)SlotType.CompoundTask);
        PopupRequest += ShowContextMenu;

        SizeFlagsHorizontal = SizeFlags.ExpandFill;
        SizeFlagsVertical = SizeFlags.ExpandFill;

        var packedScene = GD.Load<PackedScene>("res://addons/joes_htn/ActionMenu/ActionMenu.tscn");
        var am = packedScene.Instantiate();
        GD.Print($"Actual type: {am.GetType().FullName}");
        // ActionMenu.AddCompoundTaskPressed += () => AddCompoundTask(LastClickPosition);
        // ActionMenu.AddPrimitiveTaskPressed += () => AddPrimitiveTask(LastClickPosition);
        // ActionMenu.Hide();
    }

    private void ShowContextMenu(Vector2 at)
    {
        LastClickPosition = at;
        ActionMenu.Position = at;
        ActionMenu.Show();
    }

    private void AddCompoundTask(Vector2 at)
    {
        var ctn = new CompoundTaskGraphNode();
        ctn.Position = at;
        AddChild(ctn);
    }

    private void AddPrimitiveTask(Vector2 at)
    {
        var ptn = new PrimitiveTaskGraphNode();
        ptn.Position = at;
        AddChild(ptn);
    }
}
