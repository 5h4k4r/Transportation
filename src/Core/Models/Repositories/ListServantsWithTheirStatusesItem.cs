namespace Core.Models.Repositories;

public class ListServantsWithTheirStatusesResponse
{
    public int Online { get; set; }
    public int Offline { get; set; }
    public int Passive { get; set; }
    public int InTrip { get; set; }
    public int Block { get; set; }
    public List<ListServantsWithTheirStatusesItem> Items { get; set; }
}

public class ListServantsWithTheirStatusesItem
{
    public ListServantsWithTheirStatusesLocation Location { get; set; }
    public ListServantsWithTheirStatusesUser User { get; set; }
    public ListServantsWithTheirStatusesService Service { get; set; }
}

public class ListServantsWithTheirStatusesLocation
{
    public double? Lat { get; set; }
    public double? Lng { get; set; }
    public double? Bearing { get; set; }
}

public class ListServantsWithTheirStatusesUser
{
    public ulong? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
}

public class ListServantsWithTheirStatusesService
{
    public ulong? Id { get; set; }
    public string? Pin { get; set; }
}