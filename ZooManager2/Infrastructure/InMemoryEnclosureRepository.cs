using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Infrastructure;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly List<Enclosure> _enclosures = new();

    public void Add(Enclosure enclosure) => _enclosures.Add(enclosure);
    
    public Guid ConstructAndAdd(EnclosureType type, int capacity, bool isClean)
    {
        Guid id = Guid.NewGuid();
        var enclosure = new Enclosure(id, type, capacity, isClean);
        _enclosures.Add(enclosure);
        return id;
    }

    public void Remove(Guid id)
    {
        var e = _enclosures.FirstOrDefault(x => x.Id == id);
        if (e != null)
            _enclosures.Remove(e);
    }

    public Enclosure? GetById(Guid id) => _enclosures.FirstOrDefault(e => e.Id == id);

    public IEnumerable<Enclosure> GetAll() => _enclosures;

    public void Update(Enclosure enclosure)
    {
        Remove(enclosure.Id);
        Add(enclosure);
    }
}