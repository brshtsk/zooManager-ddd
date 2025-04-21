using Domain.Entities;

namespace Domain.Interfaces;

public interface IAnimalRepository
{
    Animal? GetById(Guid id);
    void Add(Animal animal);
    void Remove(Guid id);
    IEnumerable<Animal> GetAll();
}