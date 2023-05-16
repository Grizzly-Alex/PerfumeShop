namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderHeader : Entity
{
    public DateTime OrderDate { get; private set; }
    public DateTime? ShippingDate { get; private set; }
    public string CustomerId { get; private set; }
    public string? EmployeeId { get; private set; }
    public string TrackingId { get; private set; } 
	public int OrderStatusId { get; private set; }
	public OrderStatus OrderStatus { get; private set; }
    public Cost Cost { get; private set; }
    public Addressee Addressee { get; private set; }
    public PaymentDetail PaymentDetail { get; set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public OrderHeader()
	{
	}

	public OrderHeader(OrderStatuses status, List<OrderItem> orderItems, Cost cost, Addressee addressee, string customerId)
    {
        Addressee = Guard.Against.Null(addressee, nameof(addressee));
        Cost = Guard.Against.Null(cost, nameof(cost));
        OrderStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
        CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));;
        orderItems.ForEach(i => i.OrderId = Guard.Against.Null(Id, nameof(Id)));
        _orderItems = Guard.Against.Null(orderItems, nameof(orderItems));
        OrderDate = DateTime.UtcNow;
        TrackingId = Guid.NewGuid().ToString();       
    }

    public void SetEmployeeId(string employeeId) => EmployeeId = Guard.Against.NullOrEmpty(employeeId, nameof(employeeId));
    public void SetShippingDate(DateTime dateTime) => ShippingDate = Guard.Against.Null(dateTime, nameof(dateTime));
    public void SetOrderStatus(OrderStatuses status) => OrderStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));    
}