namespace Core.Models.Common;

public enum ErrorCode
{
    Unknown = -1,
    Success = 0,

    ResourceDoesNotExist,
    ResourceAlreadyExists,
    CouldNotCreateResource,
    CouldNotUpdateResource,
    CouldNotDeleteResource,
    AuthenticationFailed,
    UsernameAlreadyExists,
    PhoneNumberAlreadyExists,
    UserNotVerified,
    UserAlreadyVerified,
    OldPasswordIncorrect,
    NewPasswordEqualsOldPassword,
    PermissionDenied
}