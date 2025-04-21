using Domain.Entities;

namespace Domain.Interfaces;

public interface IFeedingScheduleRepository
{
    FeedingSchedule? GetById(Guid id);
    void Add(FeedingSchedule schedule);
    void Remove(Guid id);
    IEnumerable<FeedingSchedule> GetAll();
}