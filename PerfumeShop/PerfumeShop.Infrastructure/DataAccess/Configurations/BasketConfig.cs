namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class BasketConfig : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.Metadata.FindNavigation(nameof(Basket.Items))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(b => b.BuyerId)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.CreateDate)
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.UpdateDate)
            .HasColumnType("datetime2(7)");
    }
}
