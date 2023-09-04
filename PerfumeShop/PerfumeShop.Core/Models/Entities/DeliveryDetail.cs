namespace PerfumeShop.Core.Models.Entities;

public sealed class DeliveryDetail : Entity
{
    public DateTime? DeliveryDate { get; private set; }
    public Address DeliveryAddress { get; private set; }
    public int DeliveryMethodId { get; private set; }
    public DeliveryMethod DeliveryMethod { get; private set; }
    public int OrderId { get; private set; }
    public OrderHeader Order { get; private set; }

    public DeliveryDetail()
    {
    }

    public DeliveryDetail(Address address, DeliveryMethods method, int orderId)
    {
        DeliveryAddress = Guard.Against.Null(address, nameof(address));
        DeliveryMethodId = Guard.Against.NegativeOrZero((int)method, nameof(method));
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
    }

    public void SetDeliveryDate(DateTime date)
        => DeliveryDate = Guard.Against.Null(date, nameof(date));
}