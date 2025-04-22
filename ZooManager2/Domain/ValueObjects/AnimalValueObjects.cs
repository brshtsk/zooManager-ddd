namespace Domain.ValueObjects;

public enum AnimalType
{
    Predator,
    Herbivore,
    Bird,
    Fish
}

public enum AnimalGender
{
    Male,
    Female
}

public struct AnimalFood
{
    public string Name { get; }
    public int Quantity { get; set; }

    public AnimalFood(string foodName, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Количество еды должно быть больше 0.");
        Name = foodName;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Name} ({Quantity})";
    }

    /// <summary>
    /// Забирает часть еды из текущего запаса и возвращает новую порцию еды.
    /// </summary>
    /// <param name="portionSize">Размер новой порции</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public AnimalFood SplitPortion(int portionSize)
    {
        if (portionSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(portionSize),
                "Невалидный размер порции.");
        if (portionSize > Quantity)
            throw new ArgumentOutOfRangeException(nameof(portionSize),
                "Размер порции больше доступного количества еды.");

        Quantity -= portionSize;
        return new AnimalFood(Name, portionSize);
    }
}