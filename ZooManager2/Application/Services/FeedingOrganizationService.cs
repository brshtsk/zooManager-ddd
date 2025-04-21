using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using Domain.Events;

namespace Application.Services;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _schedules;
    private readonly IEventDispatcher _dispatcher;

    public FeedingOrganizationService(IFeedingScheduleRepository schedules, IEventDispatcher dispatcher)
    {
        _schedules = schedules;
        _dispatcher = dispatcher;
    }

    public void ScheduleFeeding(FeedingSchedule schedule)
    {
        _schedules.Add(schedule);
    }

    public IEnumerable<FeedingSchedule> GetSchedule() => _schedules.GetAll();

    public void RemoveSchedule(Guid id)
    {
        var schedule = _schedules.GetById(id);
        if (schedule == null) return;

        _schedules.Remove(id);
    }
    
    public void UpdateSchedule(Guid id, FeedingSchedule updatedSchedule)
    {
        var schedule = _schedules.GetById(id);
        if (schedule == null) return;

        schedule.Update(updatedSchedule);
        _schedules.Add(schedule);
    }
    
    public void ExecuteScheduledFeeding(Guid id)
    {
        var schedule = _schedules.GetById(id);
        if (schedule == null) return;

        schedule.Execute();
        _dispatcher.Dispatch(new FeedingTimeEvent(schedule.AnimalToFeed, schedule.Time));
    }
}