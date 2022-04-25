namespace Tranportation.Api.Responses;

public class ListServantsOnlineHistoryResponse
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Id { get; set; }
    public TimeSpan OnlineHours { get; set; }

    public ListServantsOnlineHistoryResponse(string firstName, string lastName, int id, TimeSpan onlineHours)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        OnlineHours = onlineHours;
    }
}