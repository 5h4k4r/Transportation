namespace Transportation.Api.Responses;


public class ServantPerformanceResponse
{

    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Certificate { get; set; } = string.Empty;
    public string BankId { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int DeliveredRequests { get; set; }
    public int RejectedRequests { get; set; }
    public int AcceptedRequests { get; set; }
    public int SuccessTasks { get; set; }
    public int RejectedTasks { get; set; }
    public int OnlineDurations { get; set; }
    public int DurationOnTasks { get; set; }
    public int DistanceOnTasks { get; set; }
    public List<Task>? Tasks { get; set; }
    public int Id { get; set; }
    public string Avatar { get; set; } = string.Empty;
    public List<Document>? Documents { get; set; }

}

public class Task
{
    public int CreatedAt { get; set; }
    public int UpdatedAt { get; set; }
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

