using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalTransferController : ControllerBase
{
    private readonly AnimalTransferService _transferService;

    public AnimalTransferController(AnimalTransferService transferService)
    {
        _transferService = transferService;
    }

    // Пример: POST /api/animaltransfer?animalId=xxx&newEnclosureId=yyy
    [HttpPost]
    public IActionResult MoveAnimal([FromQuery] Guid animalId, [FromQuery] Guid newEnclosureId)
    {
        _transferService.Move(animalId, newEnclosureId);
        return Ok($"Животное {animalId} переведено в вольер {newEnclosureId}");
    }
}