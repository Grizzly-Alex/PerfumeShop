﻿namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogGender : Entity
{
    public string Name { get; private set; }

    public CatalogGender(Gender gender)
    {
        Id = (int)gender;
        Name = gender.GetDisplayName();
    }

    private CatalogGender() { }

    public static implicit operator CatalogGender(Gender enumGender) => new CatalogGender(enumGender);
    public static implicit operator Gender(CatalogGender classGuitar) => (Gender)classGuitar.Id;
}
