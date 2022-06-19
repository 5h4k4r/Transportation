using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Core.Helpers;
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
            new("api-token", "hf8gew5dsnsd564dfd8f4df65df654er8561iuh5489rf74y==")
            // new("Content-Type", "application/json")
        };
    }


    public async Task<T?> Send<T>(string url, bool returnValue, bool post,
        T fields,
        List<KeyValuePair<string, string>>? headers,
        bool json = false,
        bool showError = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(fields),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);


        request.Content = new FormUrlEncodedContent(ToDictionary<string>(fields));

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


    public async Task<Dictionary<string, dynamic>> Send(string url, bool returnValue, bool post,
        dynamic fields,
        bool json = false,
        bool showError = false)
    {
        var settings = new JsonSerializerOptions { PropertyNamingPolicy = SnakeCaseNamingPolicy.Default };
        var ser = JsonSerializer.Serialize(fields, settings);
        fields = JsonSerializer.Deserialize<dynamic>(ser);
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(ToDictionary<string>(fields));

        foreach (var header in _headers) request.Headers.Add(header.Key, header.Value);

        // request.Headers.Add(HeaderNames.Accept, "application/json");
        // request.Headers.Add(HeaderNames.ContentType, "application/json");
        // request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        // headers = (List<KeyValuePair<string, string>>?)headers.Union(_headers);
        //
        // foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
        // request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
        // request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

        // _Client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        // _Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        // _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // _Client.DefaultRequestHeaders.Add("Accept", "application/json");
        // _Client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        // request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
        // _Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");


        var response = await _Client.SendAsync(request);
        var res = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(await response.Content.ReadAsStringAsync());

        var result = default(Dictionary<string, dynamic>);

        if (!json || showError)
            result = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(
                await response.Content.ReadAsStringAsync());

        Console.WriteLine($"Post: {url} {fields} {response.StatusCode} {response} {_headers}");

        return result;
    }

    public async Task<Dictionary<string, dynamic>?> Get(string url, bool json = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        // foreach (var header in headers) request.Headers.Add(header.Key, header.Value);

        var response = await _Client.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK) return default;
        return !json
            ? JsonSerializer.Deserialize<Dictionary<string, dynamic>>(await response.Content.ReadAsStringAsync())
            : default;
    }


    private static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, TValue>>(json);
        return dictionary;
    }
}