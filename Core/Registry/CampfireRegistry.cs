using System.Collections.Generic;
using Godot;
using System.Linq;

public partial class CampfireRegistry : Node
{
    public static CampfireRegistry Instance { get; private set; } = null!;
    protected List<Campfire> Campfires { get; private set; } = [];

    public override void _Ready()
    {
        Instance = this;
    }

    public void Add(Campfire c)
    {
        Campfires.Add(c);

        c.TreeExiting += () => Campfires.Remove(c);
    }

    public void Remove(Campfire c)
    {
        Campfires.Remove(c);
    }

    public Campfire? GetNearestOpen(Vector3 From)
    {
        List<Campfire> campfires = Campfires
            .Where(c => c.HasOpenSeat())
            .OrderBy((n) => From.DistanceSquaredTo(n.GlobalPosition))
            .ToList();

        if (campfires.Count == 0) return null;

        return campfires.First();
    }
}
