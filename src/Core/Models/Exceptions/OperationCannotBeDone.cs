namespace Core.Models.Exceptions;

public class OperationCannotBeDone : Exception
{
    public OperationCannotBeDone(string message = "Unable to complete the operation") : base(message)
    {
    }
}