namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderHeader : Entity
{
    public DateTime OrderDate { get; private set; }
    public string? EmployeeId { get; private set; }
    public string OrderId { get; private set; }
    public int OrderStatusId { get; private set; }
	public OrderStatus OrderStatus { get; private set; }
    public Cost Cost { get; private set; }
    public Customer Customer { get; private set; }
    public PaymentDetail PaymentDetail { get; set; }
    public DeliveryDetail DeliveryDetail { get; set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public OrderHeader()
	{
	}

	public OrderHeader(
        OrderStatuses status,
        List<OrderItem> orderItems,
        Cost cost,
        Customer customer)
    {
        Customer = Guard.Against.Null(customer, nameof(customer));
        Cost = Guard.Against.Null(cost, nameof(cost));
        OrderStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
        orderItems.ForEach(i => i.OrderId = Guard.Against.Null(Id, nameof(Id)));
        _orderItems = Guard.Against.Null(orderItems, nameof(orderItems));
        OrderDate = DateTime.UtcNow;
        OrderId = Guid.NewGuid().ToString();       
    }

    public void SetEmployeeId(string employeeId) => EmployeeId = Guard.Against.NullOrEmpty(employeeId, nameof(employeeId));
    public void SetOrderStatus(OrderStatuses status) => OrderStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));    
}