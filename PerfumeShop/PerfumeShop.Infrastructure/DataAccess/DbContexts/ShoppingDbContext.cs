namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class ShoppingDbContext : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
	public DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		#region Seeds
		modelBuilder.SeedEnumValues<OrderStatuses, OrderStatus>(value => value);
		modelBuilder.SeedEnumValues<PaymentStatuses, PaymentStatus>(value => value);
		#endregion

		#region Configurations
		modelBuilder.ApplyConfiguration(new OrderStatusConfig());
		modelBuilder.ApplyConfiguration(new PaymentStatusConfig());
		modelBuilder.ApplyConfiguration(new BasketConfig());
        modelBuilder.ApplyConfiguration(new BasketItemConfig());
        modelBuilder.ApplyConfiguration(new OrderConfig());
		modelBuilder.ApplyConfiguration(new OrderItemConfig());
        modelBuilder.ApplyConfiguration(new OrderDetailsConfig());
        modelBuilder.ApplyConfiguration(new PaymentConfig());
        #endregion
    }
}
