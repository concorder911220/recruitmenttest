using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using RecruitmentTest.Application.Responses;
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

    public async Task<CountBackListResponse> GetCountBackAsync(string instrumentId, string provider, int interval, string periodicity, int barsCount)
    {
        var credentials = await _authService.GetCredentialsAsync();

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"{_options.Value.URI}/api/bars/v1/bars/count-back?instrumentId={instrumentId}&provider={provider}&interval={interval}&periodicity={periodicity}&barsCount={barsCount}");

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);

        var response = await SendRequestAsync(httpRequestMessage);
        var countBack = await response.Content.ReadFromJsonAsync<CountBackListResponse>();

        return countBack!;
    }

    public async Task<CountBackListResponse> GetDateRangeAsync(string instrumentId, string provider, int interval, string periodicity, string startDate)
    {
        var credentials = await _authService.GetCredentialsAsync();

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            $"{_options.Value.URI}/api/bars/v1/bars/date-range?instrumentId={instrumentId}&provider={provider}&interval={interval}&periodicity={periodicity}&startDate={startDate}");

        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentials.AccessToken);

        var response = await SendRequestAsync(httpRequestMessage);
        var dateRange = await response.Content.ReadFromJsonAsync<CountBackListResponse>();

        return dateRange!;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage)
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.SendAsync(httpRequestMessage);
        response.EnsureSuccessStatusCode();
        return response;
    }


}