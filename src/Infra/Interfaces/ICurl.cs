namespace Infra.Interfaces;

public interface ICurl
{
    public Task<T?> Send<T>(
        string url,
        bool returnValue,
        bool post,
        T fields,
        List<KeyValuePair<string, string>> headers,
        bool json = false,
        bool showError = false);

    public Task<T?> Send<T>(
        string url,
        bool returnValue,
        bool post,
        T fields,
        bool json = false,
        bool showError = false);

    public Task<T?> Get<T>(string url, List<KeyValuePair<string, string>>? headers, bool json = false);
}