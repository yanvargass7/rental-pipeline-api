using RentalPipeline.Domain.Exceptions;

namespace RentalPipeline.Domain.Models;

public class Property
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Address { get; private set; }
    public PropertyType Type { get; private set; }
    public PropertyStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Property(
        string title,
        string description,
        string address,
        PropertyType type)
    {
        Title = title;
        Description = description;
        Address = address;
        Type = type;
        Status = PropertyStatus.Available;
        CreatedAt = DateTime.UtcNow;
    }

    public enum PropertyStatus
    {
        Available = 1,
        InNegotiation = 2,
        Rented = 3
    }

    public enum PropertyType
    {
        Apartment = 1,
        House = 2,
        Commercial = 3
    }

    public void MarkAsInNegotiation()
    {
        if (Status != PropertyStatus.Available)
            throw new BusinessRuleException(
                "Property is not available");

        Status = PropertyStatus.InNegotiation;
    }

    public void MarkAsRented()
    {
        Status = PropertyStatus.Rented;
    }

    public void MarkAsAvailable()
    {
        Status = PropertyStatus.Available;
    }
}