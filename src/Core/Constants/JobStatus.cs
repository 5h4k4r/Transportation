namespace Core.Constants;

public static class JobStatus
{
    public enum DestinationStatus
    {
        Canceled = -1,
        Active = 1,
        Switched = 2,
        SwitchedDestination = 3,
        Stop = 5,
        Arrived = 10
    }

    public enum RequestStatus
    {
        Waiting = 1,
        Reserve = 2,
        Doing = 3,
        Complete = 10,
        Negatives = 0
    }

    public enum ServantStatus
    {
        online,
        offline,
        passive,
        block
    }

    public enum TaskStatus
    {
        Reserve = 1,
        Accept = 2,
        Arrive = 3,
        Start = 4,
        Stop = 5,
        EndDestination = 10,
        End = 20,
        Negatives = 0
    }
}