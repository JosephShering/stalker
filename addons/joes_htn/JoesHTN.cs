#if TOOLS
using Godot;

[Tool]
public partial class JoesHTN : EditorPlugin
{
    private EditorDock GraphEditorDock = null!;

    public override void _EnterTree()
    {
        var graph = new CompoundTaskGraphEdit();
        GraphEditorDock = new EditorDock();
        GraphEditorDock.Title = "HTN Graph";
        GraphEditorDock.DefaultSlot = EditorDock.DockSlot.Bottom;
        GraphEditorDock.AddChild(graph);

        AddDock(GraphEditorDock);
    }

    public override void _ExitTree()
    {
        RemoveDock(GraphEditorDock);
    }
}
#endif
