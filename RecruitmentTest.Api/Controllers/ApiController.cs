using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Application.Services;

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
}
