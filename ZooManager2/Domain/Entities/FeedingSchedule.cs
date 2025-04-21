using Domain.ValueObjects;

namespace Domain.Entities;

public class FeedingSchedule
{
    public Guid Id { get; }
    public Animal AnimalToFeed { get; private set; }
    public FeedingTime Time { get; private set; }
    public AnimalFood AvailableFood { get; private set; }
    public bool IsActual { get; private set; }

    public FeedingSchedule(Guid id, Animal animalToFeed, FeedingTime time, AnimalFood availableFood)
    {
        if (animalToFeed.FavouriteFood.Name != availableFood.Name)
            throw new InvalidOperationException("Это животное ест только свою любимую еду, " +
                                                animalToFeed.FavouriteFood.Name);
        if (availableFood.Quantity < animalToFeed.FavouriteFood.Quantity)
            throw new InvalidOperationException("Недостаточно еды для кормления животного, минимальный размер порции " +
                                                animalToFeed.FavouriteFood.Quantity);

        Id = id;
        AnimalToFeed = animalToFeed;
        Time = time;
        AvailableFood = availableFood;
        IsActual = true;
    }

    public void Execute()
    {
        if (!IsActual)
            throw new InvalidOperationException("Это действие из расписания уже выполнено или отменено.");

        AnimalToFeed.Feed(AvailableFood);
        AvailableFood.SplitPortion(AnimalToFeed.FavouriteFood.Quantity);
        IsActual = false;
    }

    public void MarkCompleted() => IsActual = false;

    public void ChangeTime(FeedingTime newTime)
    {
        Time = newTime;
    }

    public void Update(FeedingSchedule schedule)
    {
        if (schedule == null) throw new ArgumentNullException(nameof(schedule));

        AnimalToFeed = schedule.AnimalToFeed;
        Time = schedule.Time;
        AvailableFood = schedule.AvailableFood;
        IsActual = schedule.IsActual;
    }
}