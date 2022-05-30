namespace Core.Interfaces;

public interface ICurl
{
    public abstract Task<T?> Send<T>(string url, bool returnValue, bool post,
        List<KeyValuePair<string, string>> fields,
        List<KeyValuePair<string, string>> headers,
        bool json = false,
        bool showError = false);

    public abstract Task<T?> Get<T>(string url, List<KeyValuePair<string, string>> headers, bool json = false);
}