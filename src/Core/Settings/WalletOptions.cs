namespace Core.Settings;

public class WalletOptions
{
    public const string Config = "Wallet";

    public string ServerUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}