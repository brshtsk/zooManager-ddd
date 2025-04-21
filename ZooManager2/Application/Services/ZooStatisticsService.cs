using Domain.Interfaces;

namespace Application.Services;

public class ZooStatisticsService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public ZooStatisticsService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public object GetStatistics()
    {
        return new
        {
            TotalAnimals = _animals.GetAll().Count(),
            TotalEnclosures = _enclosures.GetAll().Count(),
            FreeEnclosures = _enclosures.GetAll().Count(e => !e.Animals.Any()),
            AverageAnimalsPerEnclosure = _animals.GetAll().Count() / (double)_enclosures.GetAll().Count()
        };
    }
}