namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class ShippingInfo
{
	public DateTime ShippingDate { get; set; }
	public string? TrackingNumber { get; set; }
	public string? Carrier { get; set; }
	

    public ShippingInfo()
    {       
    }
}