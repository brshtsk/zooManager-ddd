using Domain.Interfaces;
using Domain.Events;
using Application.Interfaces;

namespace Application.Services;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;
    private readonly IEventDispatcher _dispatcher;

    public AnimalTransferService(IAnimalRepository animals, IEnclosureRepository enclosures, IEventDispatcher dispatcher)
    {
        _animals = animals;
        _enclosures = enclosures;
        _dispatcher = dispatcher;
    }

    public void Move(Guid id, Guid newEnclosureId)
    {
        var animal = _animals.GetById(id);
        var newEnclosure = _enclosures.GetById(newEnclosureId);
        if (animal == null || newEnclosure == null) return;

        animal.MoveToEnclosure(newEnclosureId);
        _dispatcher.Dispatch(new AnimalMovedEvent(animal, newEnclosure));
    }
}
