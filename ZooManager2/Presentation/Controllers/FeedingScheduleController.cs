using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingOrganizationService _feedingService;
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IAnimalRepository _animalRepository;

    public FeedingScheduleController(IFeedingOrganizationService feedingService,
        IFeedingScheduleRepository feedingScheduleRepository, IAnimalRepository animalRepository)
    {
        _feedingService = feedingService;
        _feedingScheduleRepository = feedingScheduleRepository;
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public IActionResult GetSchedule()
    {
        try
        {
            var schedule = _feedingService.GetSchedule();
            return Ok(schedule);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddSchedule(Guid animalId, TimeOnly feedingTime, string foodName, int foodQuantity)
    {
        try
        {
            var animal = _animalRepository.GetById(animalId);
            if (animal == null)
                return NotFound($"Животное с ID {animalId} не найдено.");
            var time = new FeedingTime(feedingTime);
            var food = new AnimalFood(foodName, foodQuantity);
            var id = _feedingScheduleRepository.ConstructAndAssign(animal, time, food);
            var schedule = _feedingScheduleRepository.GetById(id);
            _feedingService.ScheduleFeeding(schedule);
            return Ok($"Добавлен элемент расписания. ID: {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSchedule(Guid id, TimeOnly feedingTime, string foodName, int foodQuantity)
    {
        try
        {
            var old = _feedingScheduleRepository.GetById(id);
            if (old == null)
                return NotFound($"Элемент расписания с ID {id} не найден.");
            var animal = _animalRepository.GetById(old.AnimalToFeed.Id);
            if (animal == null)
                return NotFound($"Животное с ID {old.AnimalToFeed.Id} не найдено.");
            var time = new FeedingTime(feedingTime);
            var food = new AnimalFood(foodName, foodQuantity);
            var updated = new FeedingSchedule(id, animal, time, food);
            _feedingService.UpdateSchedule(id, updated);
            return Ok("Обновлен элемент расписания.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSchedule(Guid id)
    {
        try
        {
            _feedingService.RemoveSchedule(id);
            return Ok("Удален элемент расписания.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/execute")]
    public IActionResult ExecuteFeeding(Guid id)
    {
        try
        {
            var schedule = _feedingScheduleRepository.GetById(id);
            if (schedule == null)
                return NotFound($"Элемент расписания с ID {id} не найден.");
            var animal = _animalRepository.GetById(schedule.AnimalToFeed.Id);
            if (animal == null)
                return NotFound($"Животное с ID {schedule.AnimalToFeed.Id} не найдено.");
            _feedingService.ExecuteScheduledFeeding(id);
            return Ok($"Покормили животное {animal.Name} согласно элементу расписания {id}!.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}