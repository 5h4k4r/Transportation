namespace Core.Models.Repositories;


public class ListTasks
{
    public ulong Id { get; set; }
    public ulong RequestId { get; set; }
    public int Price { get; set; }
    public int Tip { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public TaskDistance? TaskDistanceAndDuration { get; set; }
    public ListTasksServant? Servant { get; set; }
    public Requester? Requester { get; set; }
}



public class ListTasksServant
{
    public ulong? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
}

public class Requester
{
    public Requester()
    {
    }

    public Requester(ulong? id, string? mobile, string? name, int? status)
    {
        Id = id;
        Mobile = mobile;
        Name = name;
        Status = status;
    }

    public ulong? Id { get; set; }
    public int? Status { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Mobile { get; set; } = string.Empty;
    public string? PaymentType { get; set; }
}

public class TaskDistance
{
    public ulong? Distance { get; set; }
    public ulong? Duration { get; set; }
}
