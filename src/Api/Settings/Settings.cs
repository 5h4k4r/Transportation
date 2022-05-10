using Core.Index;

namespace Api.Settings;

public class SettingsConfig
{

    public const string Config = "Settings";
    public AuthServer? AuthServer { get; set; }
    public AuthEndpoints? AuthEndpoints { get; set; }

}
public class AuthServer
{
    public string AuthUrl { get; set; } = string.Empty;
    public string ServiceId { get; set; } = string.Empty;
}