using System.ComponentModel.DataAnnotations;

namespace Core.Settings;

public class WalletOptions
{
    public const string Config = "Wallet";

    [Required] public string ServerUrl { get; set; } = string.Empty;
    [Required] public string Token { get; set; } = string.Empty;
}