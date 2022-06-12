using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Core.Interfaces;
using Infra.Interfaces;

namespace Infra.Repositories;

public class Curl : ICurl
{
    private readonly HttpClient _Client;

    public Curl(HttpClient client)
    {
        this._Client = client;
    }

    public async Task<T?> Send<T>(string url, bool returnValue, bool post,
        List<KeyValuePair<string, string>> fields,
        List<KeyValuePair<string, string>> headers,
        bool json = false,
        bool showError = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(fields);
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }


        var response = await _Client.SendAsync(request);

        var result = default(T);
        if (response.StatusCode is HttpStatusCode.OK or HttpStatusCode.Created)
        {
            if (!json)
            {
                result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }
        }
        else if (showError)
        {
            result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        Console.WriteLine($"Post: {url} {fields} {response.StatusCode} {response} {headers}");

        return result;
    }

    public async Task<T?> Get<T>(string url, List<KeyValuePair<string, string>> headers, bool json = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        var response = await _Client.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK) return default(T);
        return !json ? JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync()) : default(T);
    }
}