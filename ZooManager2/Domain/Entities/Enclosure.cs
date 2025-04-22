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
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Вместимость должна быть больше нуля.");
        Id = id;
        Type = type;
        Capacity = capacity;
        IsClean = isClean;
    }

    public bool AddAnimal(Animal animal)
    {
        if (_animalIds.Count >= Capacity) throw new Exception("В вольере недостаточно места для нового животного");
        if (_animalIds.Contains(animal.Id)) throw new Exception("Это животное уже находится в вольере");
        switch (Type)
        {
            case EnclosureType.Herbivore:
                if (animal.Type != AnimalType.Herbivore) throw new Exception("Неподходящий тип животного");
                break;
            case EnclosureType.Predator:
                if (animal.Type != AnimalType.Predator) throw new Exception("Неподходящий тип животного");
                break;
            case EnclosureType.Bird:
                if (animal.Type != AnimalType.Bird) throw new Exception("Неподходящий тип животного");
                break;
            case EnclosureType.Aquarium:
                if (animal.Type != AnimalType.Fish) throw new Exception("Неподходящий тип животного");
                break;
        }

        _animalIds.Add(animal.Id);
        return true;
    }

    public bool RemoveAnimal(Guid animalSpecies) => _animalIds.Remove(animalSpecies);
    public void Clean() => IsClean = true;
}