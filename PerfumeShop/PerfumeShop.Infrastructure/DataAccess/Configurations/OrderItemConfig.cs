namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
		builder.Property(p => p.Id)
			.IsRequired(true);

		builder.Property(p => p.ProductId)
			.IsRequired(true);

		builder.Property(p => p.Price)
			.IsRequired(true)
			.HasColumnType("decimal")
			.HasPrecision(10, 2);		
	}
}