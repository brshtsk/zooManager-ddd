using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Infrastructure;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly List<FeedingSchedule> _schedules = new();

    public void Add(FeedingSchedule schedule) => _schedules.Add(schedule);
    
    public Guid ConstructAndAssign(Animal animalToFeed, FeedingTime time, AnimalFood availableFood)
    {
        Guid id = Guid.NewGuid();
        var schedule = new FeedingSchedule(id, animalToFeed, time, availableFood);
        _schedules.Add(schedule);
        return id;
    }

    public void Remove(Guid id)
    {
        var schedule = _schedules.FirstOrDefault(s => s.Id == id);
        if (schedule != null)
            _schedules.Remove(schedule);
    }

    public IEnumerable<FeedingSchedule> GetAll() => _schedules;

    public IEnumerable<FeedingSchedule> GetByAnimalId(Guid animalId)
        => _schedules.Where(s => s.AnimalToFeed.Id == animalId);

    public FeedingSchedule? GetById(Guid id)
        => _schedules.FirstOrDefault(s => s.Id == id);
}