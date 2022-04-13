namespace Transportation.Api.Repositories;


public class ServantPerformance
{

    public Servant? Servant { get; set; }
    public int DeliveredRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int AcceptedRequests { get; set; }
    public int SuccessTasks { get; set; }
    public int RejectedTasks { get; set; }
    public int OnlineDurations { get; set; }
    public int DurationOnTasks { get; set; }
    public int DistanceOnTasks { get; set; }
    public IEnumerable<Task>? Tasks { get; set; }
    public int Id { get; set; }
    public IEnumerable<Document>? Documents { get; set; }

}

public class Task
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Distance { get; set; }
    public int Duration { get; set; }
}

public class Document
{
    public int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int IsVerified { get; set; }
    public string ModelType { get; set; } = string.Empty;
    public int ModelId { get; set; }
}

public class Servant
{
    public ulong UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string? Certificate { get; set; } = string.Empty;
    public string? BankId { get; set; } = string.Empty;
    public uint AreaId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}