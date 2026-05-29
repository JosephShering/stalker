using System;
using Godot;

[Tool]
[GlobalClass]
public partial class ActionMenu : Control
{
    public event Action? AddCompoundTaskPressed;
    public event Action? AddPrimitiveTaskPressed;

    [Export]
    private Button AddCompoundTaskButton = null!;

    [Export]
    private Button AddPrimitiveTaskButton = null!;

    public override void _Ready()
    {
        AddPrimitiveTaskButton.Pressed += () => AddPrimitiveTaskPressed?.Invoke();
        AddCompoundTaskButton.Pressed += () => AddCompoundTaskPressed?.Invoke();
    }
}
