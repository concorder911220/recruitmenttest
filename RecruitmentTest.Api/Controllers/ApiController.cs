using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Application.Services;
using System.ComponentModel;

namespace RecruitmentTest.Api.Controllers;

[Route("/api")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly MarketAssetsService _marketAssetsService;

    public ApiController(MarketAssetsService marketAssetsService)
    {
        _marketAssetsService = marketAssetsService;
    }

    [HttpGet("kinds")]
    public async Task<IResult> GetKinds()
    {
        var response = await _marketAssetsService.GetKindsAsync();
        return Results.Json(response);
    }
    
    [HttpGet("providers")]
    public async Task<IResult> GetProviders()
    {
        var response = await _marketAssetsService.GetProvidersAsync();
        return Results.Json(response);
    }
    
    [HttpGet("instruments")]
    public async Task<IResult> GetInstruments([FromQuery] [DefaultValue("oanda")] string provider, [FromQuery] [DefaultValue("forex")] string kind)
    {
        var response = await _marketAssetsService.GetInstrumentsAsync(provider, kind);
        return Results.Json(response);
    }

    [HttpGet("countBack")]
    public async Task<IResult> GetCountBack([FromQuery][DefaultValue("ad9e5345-4c3b-41fc-9437-1d253f62db52")] string instrumentId, [FromQuery][DefaultValue("oanda")] string provider, [FromQuery][DefaultValue(1)] int interval, [FromQuery][DefaultValue("minute")] string periodicity, [FromQuery] [DefaultValue(10)] int barsCount)
    {
        var response = await _marketAssetsService.GetCountBackAsync(instrumentId, provider, interval, periodicity, barsCount);
        return Results.Json(response);
    }

    [HttpGet("dateRange")]
    public async Task<IResult> GetDateRnage([FromQuery][DefaultValue("ad9e5345-4c3b-41fc-9437-1d253f62db52")] string instrumentId, [FromQuery][DefaultValue("oanda")] string provider, [FromQuery][DefaultValue(1)] int interval, [FromQuery][DefaultValue("minute")] string periodicity, [FromQuery][DefaultValue("2024-07-07")] string startDate)
    {
        var response = await _marketAssetsService.GetDateRangeAsync(instrumentId, provider, interval, periodicity, startDate);
        return Results.Json(response);
    }
}
