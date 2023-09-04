namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class BasketItemConfig : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(p => p.CreateDate)
            .HasColumnType("datetime2(7)");
    }
}
