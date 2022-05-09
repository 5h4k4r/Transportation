namespace Infra.Repositories;

public class ListServantsOnlineHistory
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public ulong? Id { get; set; }
    public TimeSpan? OnlineHours { get; set; }

    public ListServantsOnlineHistory(string firstName, string lastName, ulong id, TimeSpan onlineHours)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        OnlineHours = onlineHours;
    }
}