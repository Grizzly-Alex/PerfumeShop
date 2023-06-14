using PaymentMethod = PerfumeShop.Core.Models.Entities.PaymentMethod;
namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class SaleDbContext : DbContext
{
    public DbSet<PhysicalShop> PhysicalShops { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
	public DbSet<OrderHeader> OrderHeaders { get; set; }
	public DbSet<OrderStatus> OrderStatuses { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }
	public DbSet<PaymentStatus> PaymentStatuses { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		#region Seeds
		modelBuilder.SeedEnumValues<OrderStatuses, OrderStatus>(value => value);
		modelBuilder.SeedEnumValues<PaymentStatuses, PaymentStatus>(value => value);
        modelBuilder.SeedEnumValues<PaymentMethods, PaymentMethod>(value => value);
        modelBuilder.SeedEnumValues<DeliveryMethods, DeliveryMethod>(value => value);
        #endregion

        #region Configurations
        modelBuilder.ApplyConfiguration(new PhysicalShopConfig());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfig());
        modelBuilder.ApplyConfiguration(new OrderDeliveryMethodConfig());
        modelBuilder.ApplyConfiguration(new OrderStatusConfig());
		modelBuilder.ApplyConfiguration(new PaymentStatusConfig());
		modelBuilder.ApplyConfiguration(new BasketConfig());
        modelBuilder.ApplyConfiguration(new BasketItemConfig());
        modelBuilder.ApplyConfiguration(new OrderHeaderConfig());
		modelBuilder.ApplyConfiguration(new OrderItemConfig());
        modelBuilder.ApplyConfiguration(new PaymentDetailConfig());
        #endregion
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>()
            .HaveColumnType("time(0)");

        configurationBuilder.Properties<List<DayOfWeek>>()
            .HaveConversion<EnumConverter<DayOfWeek>>();
	}
}
