namespace Infra.Models;

public class AddServantToWalletServiceRequest
{
    public string userId { get; set; }
    public string IBan { get; set; }
    public string group { get; set; }
}