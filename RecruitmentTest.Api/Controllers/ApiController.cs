using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Application.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    public async Task<IResult> GetInstruments(
        [FromQuery]
        [DefaultValue("oanda")]
        [Required]
        string provider, 
        [FromQuery] 
        [DefaultValue("forex")]
        [Required] string kind, 
        [FromQuery] string? symbol,
        [FromQuery] int? page,
        [FromQuery] int? size
    )
    {
        var response = await _marketAssetsService.GetInstrumentsAsync(provider, kind, symbol, page, size);
        return Results.Json(response);
    }

    [HttpGet("countBack")]
    public async Task<IResult> GetCountBack(
        [FromQuery]
        [DefaultValue("ad9e5345-4c3b-41fc-9437-1d253f62db52")]
        [Required] 
        string instrumentId, 
        [FromQuery]
        [DefaultValue("oanda")]
        [Required] 
        string provider, 
        [FromQuery]
        [DefaultValue(1)]
        [Required]
        int interval, 
        [FromQuery]
        [DefaultValue("minute")]
        [Required]
        string periodicity, 
        [FromQuery] 
        [DefaultValue(10)]
        [Required]
        int barsCount
    )
    {
        var response = await _marketAssetsService.GetCountBackAsync(instrumentId, provider, interval, periodicity, barsCount);
        return Results.Json(response);
    }

    [HttpGet("dateRange")]
    public async Task<IResult> GetDateRnage(
        [FromQuery]
        [DefaultValue("ad9e5345-4c3b-41fc-9437-1d253f62db52")]
        [Required]
        string instrumentId, 
        [FromQuery]
        [DefaultValue("oanda")]
        [Required]
        string provider, 
        [FromQuery]
        [DefaultValue(1)]
        [Required]
        int interval, 
        [FromQuery]
        [DefaultValue("minute")]
        [Required]
        string periodicity, 
        [FromQuery]
        [DefaultValue("2024-07-07")]
        [Required]
        string startDate
    )
    {
        var response = await _marketAssetsService.GetDateRangeAsync(instrumentId, provider, interval, periodicity, startDate);
        return Results.Json(response);
    }
}
