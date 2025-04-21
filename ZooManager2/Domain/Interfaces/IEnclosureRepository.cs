using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IEnclosureRepository
{
    Enclosure? GetById(Guid id);
    void Add(Enclosure enclosure);
    void Remove(Guid id);
    IEnumerable<Enclosure> GetAll();
    void ConstructAndAdd(EnclosureType type, int capacity, bool isClean);
}