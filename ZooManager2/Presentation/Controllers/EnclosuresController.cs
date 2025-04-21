using Microsoft.AspNetCore.Mvc;
using Domain.ValueObjects;
using Infrastructure;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly InMemoryEnclosureRepository _enclosureRepository;

    public EnclosuresController(InMemoryEnclosureRepository enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    [HttpPost]
    public IActionResult AddEnclosure([FromQuery] string type, [FromQuery] int capacity, [FromQuery] bool isClean)
    {
        if (!Enum.TryParse<EnclosureType>(type, out var typeEnum))
            return BadRequest("Типы вольеров: Open, Cage, Aquarium, Aviary");

        _enclosureRepository.ConstructAndAdd(typeEnum, capacity, isClean);
        return Ok("Добавлен вольер.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEnclosure(Guid id)
    {
        _enclosureRepository.Remove(id);
        return Ok($"Вольер {id} удален.");
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var enclosures = _enclosureRepository.GetAll();
        return Ok(enclosures);
    }
}