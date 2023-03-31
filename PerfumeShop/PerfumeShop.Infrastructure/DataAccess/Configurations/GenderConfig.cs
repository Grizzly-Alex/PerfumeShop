﻿namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class GenderConfig : IEntityTypeConfiguration<CatalogGender>
{
    public void Configure(EntityTypeBuilder<CatalogGender> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("gender_hilo")
           .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired()
            .HasMaxLength(100);
    }
}