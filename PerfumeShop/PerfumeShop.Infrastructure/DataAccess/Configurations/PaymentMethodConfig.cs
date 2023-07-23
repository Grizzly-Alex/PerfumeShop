using PaymentMethod = PerfumeShop.Core.Models.Entities.PaymentMethod;
namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
		   .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
