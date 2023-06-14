namespace PerfumeShop.Core.Models.Entities;

public sealed class DeliveryDetail : Entity
{
    public DateTime DeliveryTime { get; set; }
    public Address DeliveryAddress { get; set; }
    public int DeliveryMethodId { get; set; }   
    public int? PickupStoreId { get; set; }   
}
