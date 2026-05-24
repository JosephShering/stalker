using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Campfire : Node3D
{
    private List<CampfireSeat> Seats = [];

    public override void _Ready()
    {
        Seats = GetChildren().OfType<CampfireSeat>().ToList();
    }

    public bool HasOpenSeat()
    {
        return Seats.Any((seat) => !seat.IsTaken);
    }

    public bool TakeFirstAvailableSeat()
    {
        var availableSeat = Seats.Find((seat) => !seat.IsTaken);
        if (availableSeat == null) return false;

        availableSeat.IsTaken = true;

        return true;
    }
}
