namespace Transportation.Api.Http;

public record ApiResponse<T>(T Data, string Code = "200", bool Error = false, bool ShowMessage = false, string Message = "");