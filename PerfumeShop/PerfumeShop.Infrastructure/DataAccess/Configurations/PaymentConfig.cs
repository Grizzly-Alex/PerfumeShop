namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(p => p.PaymentDate)
            .HasColumnName("PaymentDate")
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(false);

        builder.Property(a => a.PaymentIntentId)
            .HasColumnName("PaymentIntentId")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(a => a.SessionId)
            .HasColumnName("SessionId")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(a => a.PaymentStatusId)
            .HasColumnName("PaymentStatusId");

        builder.HasOne(a => a.PaymentStatus)
            .WithMany()
            .HasForeignKey(o => o.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}