namespace Core.Models.Repositories;

public class ListServantsOnlineHistory
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public ulong? Id { get; set; }
    public string? OnlineHours { get; set; }
    public double? TotalTimeInSeconds { get; set; }

    public ListServantsOnlineHistory(string? firstName, string? lastName, ulong id, string onlineHours, double totalTimeInSeconds)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        OnlineHours = onlineHours;
        TotalTimeInSeconds = totalTimeInSeconds;
    }
}