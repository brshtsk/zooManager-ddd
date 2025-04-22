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
    private readonly IAnimalRepository _inMemoryAnimalRepository;
    private readonly IEnclosureRepository _inMemoryEnclosureRepository;

    public AnimalsController(IAnimalRepository inMemoryAnimalRepository,
        IEnclosureRepository inMemoryEnclosureRepository)
    {
        _inMemoryAnimalRepository = inMemoryAnimalRepository;
        _inMemoryEnclosureRepository = inMemoryEnclosureRepository;
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] AddAnimalRequest request)
    {
        try
        {
            var id = _inMemoryAnimalRepository.ConstructAndAdd(
                request.Name,
                request.BirthDate,
                request.AnimalType,
                request.Gender,
                request.FavouriteFoodName,
                request.FavouriteFoodQuantity,
                request.IsHealthy,
                request.EnclosureId
            );

            try
            {
                var enclosure = _inMemoryEnclosureRepository.GetById(request.EnclosureId);
                if (enclosure == null)
                    return NotFound($"Вольер с ID {request.EnclosureId} не найден.");
                var animal = _inMemoryAnimalRepository.GetById(id);
                enclosure.AddAnimal(animal);
            }
            catch (Exception e)
            {
                return BadRequest(
                    $"Животное не получится принять в зоопарк, проверьте его данные! Ошибка при добавлении животного в вольер: {e.Message}");
            }

            return Ok($"Животное добавлено. ID: {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{animalId}")]
    public IActionResult DeleteAnimal(Guid animalId)
    {
        try
        {
            var animal = _inMemoryAnimalRepository.GetById(animalId);
            var enclosure = _inMemoryEnclosureRepository.GetById(animal.EnclosureId);
            _inMemoryAnimalRepository.Remove(animalId);
            enclosure.RemoveAnimal(animalId);
            return Ok($"Animal {animalId} deleted.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}