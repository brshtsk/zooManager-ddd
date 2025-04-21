using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooStatsController : ControllerBase
{
    private readonly ZooStatisticsService _statisticsService;

    public ZooStatsController(ZooStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public IActionResult GetZooStats()
    {
        var stats = _statisticsService.GetStatistics();
        return Ok(stats);
    }
}