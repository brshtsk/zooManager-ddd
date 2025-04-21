using Domain.Entities;

namespace Application.Interfaces;

public interface IFeedingOrganizationService
{
    void ScheduleFeeding(FeedingSchedule schedule);
    IEnumerable<FeedingSchedule> GetSchedule();
    void RemoveSchedule(Guid id);
    void UpdateSchedule(Guid id, FeedingSchedule updatedSchedule);
    void ExecuteScheduledFeeding(Guid id);
}