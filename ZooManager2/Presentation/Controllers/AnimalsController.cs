using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalTransferService _animalTransferService;
    private readonly IAnimalRepository _inMemoryAnimalRepository;

    public AnimalsController(IAnimalTransferService animalTransferService, IAnimalRepository inMemoryAnimalRepository)
    {
        _animalTransferService = animalTransferService;
        _inMemoryAnimalRepository = inMemoryAnimalRepository;
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] AddAnimalRequest request)
    {
        try
        {
            _inMemoryAnimalRepository.ConstructAndAdd(
                request.Name,
                request.BirthDate,
                request.AnimalType,
                request.Gender,
                request.FavouriteFoodName,
                request.FavouriteFoodQuantity,
                request.IsHealthy,
                request.EnclosureId
            );

            return Ok("Animal added.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{animalId}")]
    public IActionResult DeleteAnimal(Guid animalId)
    {
        _inMemoryAnimalRepository.Remove(animalId);
        return Ok($"Animal {animalId} deleted.");
    }

    [HttpPost("{animalId}/move/{enclosureId}")]
    public IActionResult MoveAnimal(Guid animalId, Guid enclosureId)
    {
        _animalTransferService.Move(animalId, enclosureId);
        return Ok();
    }
}