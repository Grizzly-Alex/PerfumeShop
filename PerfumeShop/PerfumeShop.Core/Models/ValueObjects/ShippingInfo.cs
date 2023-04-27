namespace PerfumeShop.Core.Models.ValueObjects;

public class ShippingInfo
{
	public DateTime ShippingDate { get; set; }
	public string? TrackingNumber { get; set; }
	public string? Carrier { get; set; }
	public string BuyerName { get; set; }
	public string BuyerSurname { get; set; }
	public string PhoneNumber { get; set; }
	public string PostalCode { get; set; }
	public string State { get; set; }
	public string City { get; set; }
	public string StreetAddress { get; set; }

    public ShippingInfo()
    {       
    }

    public ShippingInfo(
		string buyerName, string buyerSurname, string phoneNumber, string postalCode,
		string state, string city, string streetAddress)
    {
		BuyerName = buyerName;
		BuyerSurname = buyerSurname;
		PhoneNumber = phoneNumber;
		PostalCode = postalCode;
		State = state;
		City = city;
		StreetAddress = streetAddress;
    }
}
