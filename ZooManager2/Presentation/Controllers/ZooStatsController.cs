using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooStatsController : ControllerBase
{
    private readonly IZooStatisticsService _statisticsService;

    public ZooStatsController(IZooStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public IActionResult GetZooStats()
    {
        try
        {
            var stats = _statisticsService.GetStatistics();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}