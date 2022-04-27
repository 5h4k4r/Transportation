namespace Tranportation.Api.Responses;

public class ListServantsOnlineHistoryResponse
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public ulong? Id { get; set; }
    public double? OnlineHours { get; set; }

    public ListServantsOnlineHistoryResponse(string firstName, string lastName, ulong id, double onlineHours)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        OnlineHours = onlineHours;
    }
}