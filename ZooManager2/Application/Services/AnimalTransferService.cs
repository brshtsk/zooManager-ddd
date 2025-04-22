using Domain.Interfaces;
using Domain.Events;
using Application.Interfaces;

namespace Application.Services;

public class AnimalTransferService: IAnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public AnimalTransferService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public void Move(Guid id, Guid newEnclosureId)
    {
        var animal = _animals.GetById(id);
        var newEnclosure = _enclosures.GetById(newEnclosureId);
        var oldEnclosure = _enclosures.GetById(animal.EnclosureId);
        oldEnclosure.RemoveAnimal(animal.Id);
        newEnclosure.AddAnimal(animal);

        animal.MoveToEnclosure(newEnclosureId);
        var ev = new AnimalMovedEvent(animal, newEnclosure);
        Console.WriteLine(ev);
    }
}
