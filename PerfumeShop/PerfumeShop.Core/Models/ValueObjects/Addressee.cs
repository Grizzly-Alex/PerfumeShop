namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class Addressee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string PostalCode { get; set; }    

    public Addressee()
    {       
    }

    public Addressee(
        string firstName, string lastName,
        string state, string city, string streetAddress,
        string phoneNumber, string postalCode)
    {
        FirstName = Guard.Against.Null(firstName, nameof(firstName));
        LastName = Guard.Against.Null(lastName, nameof(lastName));
        State = Guard.Against.Null(state, nameof(state));
        City = Guard.Against.Null(city, nameof(city));
        StreetAddress = Guard.Against.Null(streetAddress, nameof(streetAddress));
        PhoneNumber = Guard.Against.Null(phoneNumber, nameof(phoneNumber));
        PostalCode = Guard.Against.Null(postalCode, nameof(postalCode));
    }
}