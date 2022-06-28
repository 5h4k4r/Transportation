namespace Core.Models.Common;

public sealed class BasicResponse
{
    /// <summary>
    ///     Represents an unknown error.
    /// </summary>
    public static readonly BasicResponse Unknown = new(ErrorCode.Unknown, "An unknown error occured.");

    /// <summary>
    ///     Represents no error, used for successful operations.
    /// </summary>
    /// <returns></returns>
    public static readonly BasicResponse Successful =
        new(ErrorCode.Success, "Request handled successfully.");

    /// <summary>
    ///     Represents error when user creation fails because username already exists.
    /// </summary>
    public static readonly BasicResponse UsernameAlreadyExists =
        new(ErrorCode.UsernameAlreadyExists, "Username already exists.");

    /// <summary>
    ///     Represents error when user creation fails because username already exists.
    /// </summary>
    public static readonly BasicResponse PhoneNumberAlreadyExists =
        new(ErrorCode.PhoneNumberAlreadyExists, "Phone number already exists.");

    /// <summary>
    ///     Represents error when a user is not verified.
    /// </summary>
    public static readonly BasicResponse UserNotVerified =
        new(ErrorCode.UserNotVerified, "User is not verified.");

    /// <summary>
    ///     Represents error when resource creation fails.
    /// </summary>
    public static readonly BasicResponse CouldNotCreateResource =
        new(ErrorCode.CouldNotCreateResource, "Could not create the requested resource.");

    /// <summary>
    ///     Represents error when updating a resource fails.
    /// </summary>
    public static readonly BasicResponse CouldNotUpdateResource =
        new(ErrorCode.CouldNotCreateResource, "Could not update the requested resource.");

    /// <summary>
    ///     Represents error when deleting a resource fails.
    /// </summary>
    public static readonly BasicResponse CouldNotDeleteResource =
        new(ErrorCode.CouldNotDeleteResource, "Could not delete the requested resource.");

    /// <summary>
    ///     Represents error when resource creation fails.
    /// </summary>
    public static readonly BasicResponse AuthenticationFailed =
        new(ErrorCode.AuthenticationFailed, "Authentication failed.");

    /// <summary>
    ///     Represents error when a required CompanyId is missing.
    /// </summary>
    public static readonly BasicResponse UserAlreadyVerified =
        new(ErrorCode.UserAlreadyVerified, "User already verified");

    public static readonly BasicResponse OldPasswordIncorrect =
        new(ErrorCode.OldPasswordIncorrect, "Old password is incorrect");

    public static readonly BasicResponse NewPasswordEqualsOldPassword =
        new(ErrorCode.NewPasswordEqualsOldPassword, "New password cannot be the same as the old password");

    public static readonly BasicResponse ResourceNotFound =
        new(ErrorCode.ResourceDoesNotExist, "Resource not found");

    public static readonly BasicResponse ResourceAlreadyExists =
        new(ErrorCode.ResourceAlreadyExists, "Resource already exists");

    public static readonly BasicResponse BadRequest =
        new(ErrorCode.ResourceAlreadyExists, "Resource already exists");

    public static readonly BasicResponse PermissionDenied =
        new(ErrorCode.PermissionDenied, "Permission denied");

    public BasicResponse(in string message)
    {
        Code = ErrorCode.Unknown;
        Message = message;
    }

    public BasicResponse(in ErrorCode code)
    {
        Code = code;
        Message = code.ToString();
    }

    public BasicResponse(in ErrorCode code, in string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    ///     Error code used to define the type of error
    /// </summary>
    public ErrorCode Code { get; }

    /// <summary>
    ///     A short message explaining what went wrong
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     Represents no error, used for successful operations.
    /// </summary>
    public static BasicResponse Success(in string message)
    {
        return new BasicResponse(ErrorCode.Success, message);
    }

    /// <summary>
    ///     Represents error when the requested resource does not exist
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="resourceId">Id of the requested resource</param>
    public static BasicResponse ResourceDoesNotExist(in string resourceName,
        in int resourceId)
    {
        return new BasicResponse(ErrorCode.ResourceDoesNotExist,
            $"Resource '{resourceName}' with id '{resourceId}' does not exist.");
    }

    public static BasicResponse ResourceDoesNotExist(in string resourceName,
        in int resourceId,
        in string referenceName)
    {
        return new BasicResponse(ErrorCode.ResourceDoesNotExist,
            $"Resource '{resourceName}' with '{referenceName}' '{resourceId}' does not exist.");
    }

    public static BasicResponse ResourceDoesNotExist(in string resourceName)
    {
        return new BasicResponse(ErrorCode.ResourceDoesNotExist,
            $"Resource '{resourceName}' does not exist.");
    }
    /// <summary>
    /// Represents error when the requested resource already exists
    /// </summary>
    /// <param name="resourceName">name of the requested resource</param>
    // public static BasicResponse DuplicateEntry(in string resourceName)
    // {
    //     return new(ErrorCode.ResourceAlreadyExists,
    //         $"Resource '{resourceName}' already exists.");
    // }
}