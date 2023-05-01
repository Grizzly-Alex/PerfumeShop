namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class PaymentStatusConfig : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
		   .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
