using System.Net;
using System.Text.Json;
using Infra.Interfaces;

namespace Infra.Repositories;

public class Curl : ICurl
{
    private readonly HttpClient _Client;
    private readonly List<KeyValuePair<string, string>> _headers;

    public Curl(HttpClient client)
    {
        _Client = client;
        // $this->headers = array("api-token:" . $this->api_token, "Content-Type: application/json");
        _headers = new List<KeyValuePair<string, string>>
        {
            new("api-token", ""),
            new("Content-Type", "application/json")
        };
    }


    public async Task<T?> Send<T>(string url, bool returnValue, bool post,
        T fields,
        List<KeyValuePair<string, string>>? headers,
        bool json = false,
        bool showError = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(ToDictionary<string>(fields));

        headers = (List<KeyValuePair<string, string>>?)headers.Union(_headers);

        foreach (var header in headers) request.Headers.Add(header.Key, header.Value);


        var response = await _Client.SendAsync(request);

        var result = default(T);
        if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
        {
            if (!json) result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        else if (showError)
        {
            result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        Console.WriteLine($"Post: {url} {fields} {response.StatusCode} {response} {headers}");

        return result;
    }

    public async Task<T?> Send<T>(string url, bool returnValue, bool post,
        T fields,
        bool json = false,
        bool showError = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(ToDictionary<string>(fields));

        // foreach (var header in _headers) request.Headers.Add(header.Key, header.Value);

        // request.Headers.Add(HeaderNames.Accept, "application/json");
        // request.Headers.Add(HeaderNames.ContentType, "application/json");

        var response = await _Client.SendAsync(request);

        var result = default(T);
        if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
        {
            if (!json) result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
        else if (showError)
        {
            result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        Console.WriteLine($"Post: {url} {fields} {response.StatusCode} {response} {_headers}");

        return result;
    }

    public async Task<T?> Get<T>(string url, List<KeyValuePair<string, string>> headers, bool json = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        foreach (var header in headers) request.Headers.Add(header.Key, header.Value);

        var response = await _Client.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK) return default;
        return !json ? JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync()) : default;
    }


    private static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, TValue>>(json);
        return dictionary;
    }
}