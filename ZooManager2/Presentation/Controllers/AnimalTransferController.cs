using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalTransferController : ControllerBase
{
    private readonly IAnimalTransferService _transferService;

    public AnimalTransferController(IAnimalTransferService transferService)
    {
        _transferService = transferService;
    }

    // Пример: POST /api/animaltransfer?animalId=xxx&newEnclosureId=yyy
    [HttpPost]
    public IActionResult MoveAnimal([FromQuery] Guid animalId, [FromQuery] Guid newEnclosureId)
    {
        try
        {
            _transferService.Move(animalId, newEnclosureId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok($"Животное {animalId} переведено в вольер {newEnclosureId}");
    }
}