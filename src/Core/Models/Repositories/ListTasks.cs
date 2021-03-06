namespace Core.Repositories;


public class ListTasks
{
    public ulong Id { get; set; }
    public ulong RequestId { get; set; }
    public int Price { get; set; }
    public int Tip { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public TaskDistance? Distance { get; set; }
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
    public ulong? Id { get; set; }
    public int? Status { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Mobile { get; set; } = string.Empty;
}

public class TaskDistance
{
    public int? Distance { get; set; }
    public int? Duration { get; set; }
}
