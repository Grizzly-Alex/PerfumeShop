namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderReceiptMethodConfig : IEntityTypeConfiguration<OrderReceiptMethod>
{
    public void Configure(EntityTypeBuilder<OrderReceiptMethod> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
		   .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
