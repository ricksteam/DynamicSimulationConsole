
using System.Net;
using System.Net.Http.Json;
using DynamicSimulationConsole.Services.Models;
using Newtonsoft.Json;

namespace DynamicSimulationConsole.Services;

public class ValhallaClient
{
    private HttpClient _httpClient;
    private bool _disposed = false;

    public ValhallaClient(string baseAddress)
    {
        Console.WriteLine(baseAddress);
        _httpClient = CreateHttpClient(baseAddress);
    }

    private HttpClient CreateHttpClient(string baseAddress)
    {
        var newHttpClient = new HttpClient();
        newHttpClient.BaseAddress = new Uri(baseAddress);
        return newHttpClient;
    }

    public async Task<ValhallaRoute[]> GetRoute(ValhallaRouteParameters parameters)
    {
        var json = JsonConvert.SerializeObject(parameters);
        var encodedJson = WebUtility.UrlEncode(json);
        var result = await _httpClient.GetAsync($"route?json={encodedJson}");
        result.EnsureSuccessStatusCode();
        var data = await result.Content.ReadFromJsonAsync<ValhallaResponse>();

        var returnList = new List<ValhallaRoute>()
        {
            new(data.trip)
        };
        returnList.AddRange(data.alternates);
        
        return returnList.ToArray();
    }
}