namespace Core.Common;

public enum TaskState
{
    //
    // Summary:
    //     Task has been reserved by a user.
    Reserve = 1,
    //
    // Summary:
    //     Task is Accepted by a driver to be activated and scheduled internally by the .NET infrastructure.
    Accept = 2,
    //
    // Summary:
    //     Driver arrived to the pickup location.
    Arrive = 3,
    //
    // Summary:
    //     Task is running but has not yet completed.
    Start = 4,
    //
    // Summary:
    //     driver has stopped in a location and waiting for the passenger
    Stop = 5,
    //
    // Summary:
    //     Driver arrived to it's destination successfully.
    EndDestination = 10,
    //
    // Summary:
    //     Task completed execution successfully
    End = 20,
    //
    // Summary:
    //     Task completed due to an unhandled exception.
    Negatives = 0

}