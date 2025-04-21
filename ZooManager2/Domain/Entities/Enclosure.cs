using Domain.ValueObjects;

namespace Domain.Entities;

public class Enclosure
{
    public Guid Id { get; }
    public EnclosureType Type { get; }
    public int Capacity { get; }
    public bool IsClean { get; private set; }
    private readonly List<Guid> _animalIds = new();

    public IReadOnlyCollection<Guid> Animals => _animalIds.AsReadOnly();

    public Enclosure(Guid id, EnclosureType type, int capacity, bool isClean)
    {
        Id = id;
        Type = type;
        Capacity = capacity;
        IsClean = isClean;
    }

    public bool AddAnimal(Animal animal)
    {
        if (_animalIds.Count >= Capacity) return false;
        if (_animalIds.Contains(animal.Id)) return false;
        switch (Type)
        {
            case EnclosureType.Herbivore:
                if (animal.Type != AnimalType.Herbivore) return false;
                break;
            case EnclosureType.Predator:
                if (animal.Type != AnimalType.Predator) return false;
                break;
            case EnclosureType.Bird:
                if (animal.Type != AnimalType.Bird) return false;
                break;
            case EnclosureType.Aquarium:
                if (animal.Type != AnimalType.Fish) return false;
                break;
        }

        _animalIds.Add(animal.Id);
        return true;
    }

    public bool RemoveAnimal(Guid animalSpecies) => _animalIds.Remove(animalSpecies);
    public void Clean() => IsClean = true;
}