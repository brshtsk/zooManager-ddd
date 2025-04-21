namespace Application.Interfaces;

public interface IAnimalTransferService
{
    void Move(Guid id, Guid newEnclosureId);
}