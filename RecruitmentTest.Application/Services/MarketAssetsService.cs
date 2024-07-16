using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RecruitmentTest.Common;

namespace RecruitmentTest.Application.Services;

public class MarketAssetsService
{
    private readonly IOptions<ApiOptions> _options;
    private readonly AuthService _authService;
    private readonly IHttpClientFactory _httpClientFactory;

    public MarketAssetsService(AuthService authService, IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        _authService = authService;
        _httpClientFactory = httpClientFactory;
        _options = options;
    }

    public async Task<KindsListResponse> GetKindsAsync()
    {
        var credentials = await _authService.GetCredentialsAsync();

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            new Uri($"{_options.Value.URI}/api/instruments/v1/instruments/kinds/"));

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
        
        var response = await SendRequestAsync(httpRequestMessage);
        var kinds = await response.Content.ReadFromJsonAsync<KindsListResponse>();

        return kinds!;
    }
    
    public async Task<ProvidersListResponse> GetProvidersAsync()
    {
        var credentials = await _authService.GetCredentialsAsync();
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"{_options.Value.URI}/api/instruments/v1/providers");

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
        
        var response = await SendRequestAsync(httpRequestMessage);
        var providers = await response.Content.ReadFromJsonAsync<ProvidersListResponse>();

        return providers!;
    }

    public async Task<InstrumentsListResponse> GetInstrumentsAsync(string provider, string kind)
    {
        var credentials = await _authService.GetCredentialsAsync();
        
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"{_options.Value.URI}/api/instruments/v1/instruments?provider={provider}&kind={kind}");

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
        
        var response = await SendRequestAsync(httpRequestMessage);
        var instruments = await response.Content.ReadFromJsonAsync<InstrumentsListResponse>();

        return instruments!;
    }
    
    private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage)
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.SendAsync(httpRequestMessage);
        response.EnsureSuccessStatusCode();
        return response;
    }
}

public class KindsListResponse
{
    public List<string>? Data { get; set; }
}

public class ProvidersListResponse
{
    public List<string>? Data { get; set; }
}

public class InstrumentsListResponse
{
    public List<InstrumentResponse>? Data { get; set; }
}

public class InstrumentResponse
{
    public Guid Id { get; set; }
    public string? Symbol { get; set; }
    public string? Kind { get; set; }
    public string? Description { get; set; }
    public double TickSize { get; set; }
    public string? Currency { get; set; }
    public string? BaseCurrency { get; set; }
    public Dictionary<string, MappingResponse>? Mappings { get; set; }
}

public class MappingResponse
{
    public string? Symbol { get; set; }
    public string? Exchange { get; set; }
    public int? DefaultOrderSize { get; set; }
}