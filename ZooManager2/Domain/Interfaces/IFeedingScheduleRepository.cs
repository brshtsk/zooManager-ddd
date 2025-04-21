using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IFeedingScheduleRepository
{
    FeedingSchedule? GetById(Guid id);
    void Add(FeedingSchedule schedule);
    void Remove(Guid id);
    IEnumerable<FeedingSchedule> GetAll();
    void ConstructAndAssign(Animal animalToFeed, FeedingTime time, AnimalFood availableFood);
    IEnumerable<FeedingSchedule> GetByAnimalId(Guid animalId);
}