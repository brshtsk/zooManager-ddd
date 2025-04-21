using Domain.Entities;

namespace Domain.Interfaces;

public interface IEnclosureRepository
{
    Enclosure? GetById(Guid id);
    void Add(Enclosure enclosure);
    void Remove(Guid id);
    IEnumerable<Enclosure> GetAll();
}