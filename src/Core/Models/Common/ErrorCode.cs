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
    DateRangeTooLong,

    // Authentication
    AuthenticationFailed,
    UsernameAlreadyExists,
    PhoneNumberAlreadyExists,
    PassswordTooShort,
    UserDeactivated,
    UserNotVerified,
    UserAlreadyVerified,
    InvalidUserRole,
    WrongOtpCode,
    InvalidToken,
    TooManyPasswordResetRequests,
    OldPasswordIncorrect,
    NewPasswordEqualsOldPassword,
    PermissionDenied

}

