using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingOrganizationService _feedingService;

    public FeedingScheduleController(IFeedingOrganizationService feedingService)
    {
        _feedingService = feedingService;
    }

    [HttpGet]
    public IActionResult GetSchedule()
    {
        var schedule = _feedingService.GetSchedule();
        return Ok(schedule);
    }

    [HttpPost]
    public IActionResult AddSchedule([FromBody] FeedingSchedule schedule)
    {
        _feedingService.ScheduleFeeding(schedule);
        return Ok("Добавлен элемент расписания.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSchedule(Guid id, [FromBody] FeedingSchedule updated)
    {
        _feedingService.UpdateSchedule(id, updated);
        return Ok("Обновлен элемент расписания.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSchedule(Guid id)
    {
        _feedingService.RemoveSchedule(id);
        return Ok("Удален элемент расписания.");
    }

    [HttpPost("{id}/execute")]
    public IActionResult ExecuteFeeding(Guid id)
    {
        _feedingService.ExecuteScheduledFeeding(id);
        return Ok($"Покормили животное согласно элементу расписания {id}!.");
    }
}