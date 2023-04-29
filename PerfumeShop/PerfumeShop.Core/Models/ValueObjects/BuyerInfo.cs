namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class BuyerInfo
{
    public string BuyerId { get; set; }
    public string BuyerName { get; set; }
    public string BuyerSurname { get; set; }
    public string PhoneNumber { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }

    public BuyerInfo()
    {       
    }

    public BuyerInfo(
        string buyerId, string buyerName, string buyerSurname,
        string state, string city, string streetAddress,
        string phoneNumber, string postalCode)
    {
        Guard.Against.Null(buyerId, nameof(buyerId));
        Guard.Against.Null(buyerName, nameof(buyerName));
        Guard.Against.Null(buyerSurname, nameof(buyerSurname));
        Guard.Against.Null(phoneNumber, nameof(phoneNumber));
        Guard.Against.Null(postalCode, nameof(postalCode));
        Guard.Against.Null(state, nameof(state));
        Guard.Against.Null(city, nameof(city));
        Guard.Against.Null(streetAddress, nameof(streetAddress));

        BuyerId = buyerId;
        BuyerName = buyerName;
        BuyerSurname = buyerSurname;
        PhoneNumber = phoneNumber;
        PostalCode = postalCode;
        State = state;
        City = city;
        StreetAddress = streetAddress;
    }
}