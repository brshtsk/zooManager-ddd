using Microsoft.AspNetCore.Mvc;
using Domain.ValueObjects;
using Infrastructure;
using Domain.Entities;
using Domain.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingSchedulesController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IFeedingScheduleRepository _scheduleRepository;

    public FeedingSchedulesController(
        IAnimalRepository animalRepository,
        IFeedingScheduleRepository scheduleRepository)
    {
        _animalRepository = animalRepository;
        _scheduleRepository = scheduleRepository;
    }

    [HttpPost]
    public IActionResult AddFeeding([FromQuery] Guid animalId, [FromQuery] string timeString,
        [FromQuery] string foodName, [FromQuery] int quantity)
    {
        var animal = _animalRepository.GetById(animalId);
        if (animal is null)
            return NotFound("Животное не найдено.");

        if (!TimeOnly.TryParse(timeString, out var parsedTime))
            return BadRequest("Формат времени: HH:mm (например, 10:30)");

        var time = new FeedingTime(parsedTime);
        var food = new AnimalFood(foodName, quantity);

        _scheduleRepository.ConstructAndAssign(animal, time, food);
        return Ok("Добавлен элемент расписания.");
    }

    [HttpGet]
    public IActionResult GetAllSchedules()
    {
        return Ok(_scheduleRepository.GetAll());
    }

    [HttpGet("by-animal/{animalId}")]
    public IActionResult GetByAnimalId(Guid animalId)
    {
        var schedules = _scheduleRepository.GetByAnimalId(animalId);
        return Ok(schedules);
    }
}