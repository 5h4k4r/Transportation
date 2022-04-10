namespace Transportation.Api.Settings;

public class VariableSettings
{

    public const string Config = "Settings";
    public AuthServer? AuthServer { get; set; }

}
public class AuthServer
{
    public string AuthUrl { get; set; } = string.Empty;
    public string ServiceId { get; set; } = string.Empty;
}