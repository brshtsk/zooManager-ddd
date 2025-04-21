using Domain.Entities;

namespace Domain.Interfaces;

public interface IAnimalRepository
{
    Animal? GetById(Guid id);
    void Add(Animal animal);
    void Remove(Guid id);
    IEnumerable<Animal> GetAll();

    void ConstructAndAdd(string name, DateTime birthDate, string animalTypeString, string genderString,
        string favouriteFoodName, int favouriteFoodQuantity, bool isHealthy, Guid enclosureId);
}