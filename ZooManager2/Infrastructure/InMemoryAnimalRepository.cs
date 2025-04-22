using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Infrastructure;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _animals = new();

    public void Add(Animal animal) => _animals.Add(animal);

    public Guid ConstructAndAdd(string name, DateTime birthDate, string animalTypeString, string genderString,
        string favouriteFoodName, int favouriteFoodQuantity, bool isHealthy, Guid enclosureId)
    {
        Guid id = Guid.NewGuid();
        var typeEnum = Enum.TryParse<AnimalType>(animalTypeString, out var type)
            ? type
            : throw new Exception("Доступны только следующие типы животных: Herbivore, Predator, Bird, Fish");
        var genderEnum = Enum.TryParse<AnimalGender>(genderString, out var gender)
            ? gender
            : throw new Exception("Доступны: Male, Female");
        var favouriteFood = new AnimalFood(favouriteFoodName, favouriteFoodQuantity);
        var birthDateOnly = DateOnly.FromDateTime(birthDate);
        var animal = new Animal(id, name, birthDateOnly, type, gender, favouriteFood, isHealthy, enclosureId);
        _animals.Add(animal);
        return id;
    }

    public void Remove(Guid id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal != null)
            _animals.Remove(animal);
    }

    public Animal? GetById(Guid id) => _animals.FirstOrDefault(a => a.Id == id);

    public IEnumerable<Animal> GetAll() => _animals;

    public void Update(Animal animal)
    {
        Remove(animal.Id);
        Add(animal);
    }
}