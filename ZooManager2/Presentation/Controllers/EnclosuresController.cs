using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.ValueObjects;
using Infrastructure;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosureRepository;

    public EnclosuresController(IEnclosureRepository enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    [HttpPost]
    public IActionResult AddEnclosure([FromQuery] string type, [FromQuery] int capacity, [FromQuery] bool isClean)
    {
        if (!Enum.TryParse<EnclosureType>(type, out var typeEnum))
            return BadRequest("Типы вольеров: Predator, Herbivore, Bird, Aquarium");

        try
        {
            var id = _enclosureRepository.ConstructAndAdd(typeEnum, capacity, isClean);
            return Ok($"Добавлен вольер. ID: {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEnclosure(Guid id)
    {
        try
        {
            var enclosure = _enclosureRepository.GetById(id);
            if (enclosure.Animals.Count > 0)
                return BadRequest($"В вольере {id} есть животные. Переместите их перед удалением вольера.");
            _enclosureRepository.Remove(id);
            return Ok($"Вольер {id} удален.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var enclosures = _enclosureRepository.GetAll();
            return Ok(enclosures);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}