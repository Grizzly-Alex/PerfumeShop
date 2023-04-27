namespace PerfumeShop.Core.Models.Entities;

public sealed class Order : Entity
{
    public DateTime OrderDate { get; set; }
	public DateTime ShippingDate { get; set; }
	public DateTime PaymentDate { get; set; }
	public DateOnly PaymentDueDate { get; set; }
	public decimal OrderTotal { get; set; }
	public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
	public string? Carrier { get; set; }
	public string FirsrtName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }
	public string PostalCode { get; set; }
	public string State { get; set; }
	public string City { get; set; }
	public string StreetAddress { get; set; }

	public string AppUserId { get; set; }

    [ValidateNever]
    public AppUser AppUser { get; set; }
}
