using Domain.ValueObjects;

namespace Domain.Entities;

public class Animal
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public AnimalType Type { get; private set; }
    public AnimalGender Gender { get; private set; }
    
    // Любимая еда животного. Считаем, что животное ест только такую еду.
    // Quantity обозначает размер минимальной порции еды.
    public AnimalFood FavouriteFood { get; private set; }
    public bool IsHealthy { get; private set; }
    public Guid EnclosureId { get; private set; }

    public Animal(Guid id, string name, DateOnly birthDate, AnimalType type, AnimalGender gender, AnimalFood favouriteFood,
        bool isHealthy, Guid enclosureId)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Type = type;
        Gender = gender;
        FavouriteFood = favouriteFood;
        IsHealthy = isHealthy;
        EnclosureId = enclosureId;
    }
    
    public void Feed(AnimalFood food)
    {
        if (food.Name != FavouriteFood.Name)
            throw new InvalidOperationException("Животное может есть только свою любимую еду.");
        
        if (food.Quantity < FavouriteFood.Quantity)
            throw new InvalidOperationException("Недостаточно еды для кормления животного.");
        
        food.SplitPortion(FavouriteFood.Quantity); // Забираем порцию еды из текущего запаса
    }
    public void Cure() => IsHealthy = true;
    public void MarkAsSick() => IsHealthy = false;
    public void MoveToEnclosure(Guid newEnclosureId) => EnclosureId = newEnclosureId;
}