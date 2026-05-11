namespace RentalPipeline.Domain.Models;
public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string DocumentNumber { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Customer(
        string name,
        string email,
        string documentNumber,
        string phoneNumber)
    {
        Name = name;
        Email = email;
        DocumentNumber = documentNumber;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }
}