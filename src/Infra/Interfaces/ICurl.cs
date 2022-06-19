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

    public Task<Dictionary<string, dynamic>> Send(string url, bool returnValue, bool post,
        dynamic fields,
        bool json = false,
        bool showError = false);

    public Task<Dictionary<string, dynamic>?> Get(string url, bool json = false);
}