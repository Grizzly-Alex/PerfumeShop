namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class Address
{
    public string State { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }

    public Address(string state, string city, string streetAddress, string postalCode)
    {
        State = Guard.Against.Null(state, nameof(state));
        City = Guard.Against.Null(city, nameof(city));
        StreetAddress = Guard.Against.Null(streetAddress, nameof(streetAddress));
        PostalCode = Guard.Against.Null(postalCode, nameof(postalCode));
    }
}
