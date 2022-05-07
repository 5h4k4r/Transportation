namespace Core.Index;

public class Setting
{
    public string Address { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
}
public class GetSettings
{
    public List<Setting> Exclude { get; set; } = new List<Setting>();
    public List<Setting> Block { get; set; } = new List<Setting>();
}