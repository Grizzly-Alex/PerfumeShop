namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class ShoppingDbContext : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }

	public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BasketConfig());
        modelBuilder.ApplyConfiguration(new BasketItemConfig());
        modelBuilder.ApplyConfiguration(new OrderConfig());
		modelBuilder.ApplyConfiguration(new OrderItemConfig());
	}
}
