namespace Presentation.Models;

public class AddAnimalRequest
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; } // ISO 8601 формат: "2024-04-20T00:00:00"
    public string AnimalType { get; set; }
    public string Gender { get; set; }
    public string FavouriteFoodName { get; set; }
    public int FavouriteFoodQuantity { get; set; }
    public bool IsHealthy { get; set; }
    public Guid EnclosureId { get; set; }
}