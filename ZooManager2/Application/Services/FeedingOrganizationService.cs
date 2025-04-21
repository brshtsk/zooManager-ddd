using Domain.Interfaces;
using Domain.Entities;
using Domain.Events;
using Application.Interfaces;

namespace Application.Services;

public class FeedingOrganizationService: IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _schedules;

    public FeedingOrganizationService(IFeedingScheduleRepository schedules)
    {
        _schedules = schedules;
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
        var ev = new FeedingTimeEvent(schedule.AnimalToFeed, schedule.Time);
        Console.WriteLine(ev);
    }
}