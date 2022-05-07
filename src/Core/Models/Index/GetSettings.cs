namespace Core.Index;

public class AuthEndpoint
{
    public string Address { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
}
public class AuthEndpoints
{
    public List<AuthEndpoint> Exclude { get; set; } = new List<AuthEndpoint>();
    public List<AuthEndpoint> Block { get; set; } = new List<AuthEndpoint>();
}